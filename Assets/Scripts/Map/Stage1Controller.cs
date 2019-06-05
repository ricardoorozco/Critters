using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class Stage1Controller : MonoBehaviour
{
	#if UNITY_ANDROID
	private string gameId = "1578407";
	#elif UNITY_IOS
	private string gameId = "";
	#elif UNITY_EDITOR
	private string gameId = "";
	#endif

	[SerializeField] int level;

	private int id;
	private int active;
	private int best_time;
	private int best_stars;
	private int profile;

	[SerializeField] private GameObject victimContainer;
	private int victimCount;

	[SerializeField] private Text killCountText;
	private int killCount = 0;

	[SerializeField] private Text timeCountText;
	[SerializeField] private float timeCount = 0;

	[SerializeField] private GameObject agentsSpawnContainer;
	[SerializeField] private GameObject agent;
	private bool agenstStart = false;

	[SerializeField] private GameObject player;
	private bool playerIsDeath = false;

	[SerializeField] private GameObject doorBlocker;

	[SerializeField] private Canvas canvas;
	[SerializeField] private Canvas canvasBlood;

	[SerializeField] private Canvas finishCanvas;
	private bool finish;
	[SerializeField] Sprite start;
	private int totalStarts = 1;
	[SerializeField] Sprite startless;
	[SerializeField] Image start1;
	[SerializeField] Image start2;
	[SerializeField] Image start3;
	[SerializeField] Text currentTimeText;
	private float currentTime = 0;
	private int deathCount = 0;
	[SerializeField] Text bestTimeText;


	[SerializeField] GameObject revive;

	// Use this for initialization
	void Start()
	{
		canvas.gameObject.SetActive(false);
		canvasBlood.gameObject.SetActive(false);

		victimCount = victimContainer.transform.childCount;
		killCountText.text = killCount + "/" + victimCount;

		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}

	public void addKill()
	{
		killCount++;
		killCountText.text = killCount + "/" + victimCount;
		if (killCount >= victimCount)
		{
			Destroy(doorBlocker);
		}
	}



	private void Update()
	{
		if (finishCanvas.gameObject.activeSelf)
		{
			if (!finish)
			{
				finish = true;
				if (deathCount == 0)
				{
					totalStarts++;
				}
				if (timeCount > 0)
				{
					totalStarts++;
				}
				start1.sprite = start;
				start2.sprite = totalStarts > 1 ? start : startless;
				start3.sprite = totalStarts > 2 ? start : startless;

				ProfileDB profiledb = new ProfileDB();
				int currentUser = profiledb.getCurrentUserId();
				LevelDB lvldb = new LevelDB();
				int[] currenData = new int[6];
				if (currentUser > 0)
				{
					currenData = lvldb.getStatusLevel(level, currentUser);
				}
				else
				{
					return;
				}

				float bestTime = (currentTime < (float)currenData[3] || currenData[3] == 0) ? currentTime : (float)currenData[3];

				float m = Mathf.Floor(bestTime / 60);
				float s = Mathf.Floor(bestTime - (m * 60));

				bestTimeText.text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');

				int bestStarts = (totalStarts > currenData[4] || currenData[4] == 0) ? totalStarts : currenData[4];

				lvldb.updateStatusLevel(level, (int)bestTime, bestStarts, currentUser);

			}
			return;
		}

		if (timeCount == 0 && !agenstStart)
		{
			startAgent();
			agenstStart = true;
		}

		if (!player.activeSelf)
		{
			if (playerIsDeath)
			{
				return;
			}
			playerIsDeath = true;
			deathCount++;
			canvasBlood.gameObject.SetActive(!canvasBlood.gameObject.activeSelf);
			StartCoroutine("stopGame");
		}
	}

	private void FixedUpdate()
	{
		if (finish)
		{
			return;
		}
		timeCount -= Time.deltaTime;

		currentTime += Time.deltaTime;

		float m = Mathf.Floor(currentTime / 60);
		float s = Mathf.Floor(currentTime - (m * 60));

		currentTimeText.text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');

		if (timeCount > 0)
		{

			m = Mathf.Floor(timeCount / 60);
			s = Mathf.Floor(timeCount - (m * 60));

			if (timeCount < 15)
			{
				timeCountText.color = new Color(0.5f, 0, 0, 1);
			}
			else if (timeCountText.color == new Color(0.5f, 0, 0, 1))
			{
				timeCountText.color = Color.white;
			}
			timeCountText.text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');
		}
		else
		{
			timeCountText.text = "00:00";
			timeCount = 0;
		}
	}

	public void startAgent()
	{
		for (int i = 0; i < agentsSpawnContainer.transform.childCount; i++)
		{
			Instantiate(agent, agentsSpawnContainer.transform.GetChild(i).position, Quaternion.identity);
		}
	}

	IEnumerator stopGame()
	{
		yield return new WaitForSeconds(1);
		Time.timeScale = 0;
		canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void Revive()
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        canvasBlood.gameObject.SetActive(!canvasBlood.gameObject.activeSelf);
        Time.timeScale = 1;
        playerIsDeath = false;
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
        for (int i = 0; i < agents.Length; i++)
        {
            Destroy(agents[i]);
        }
        player.SetActive(true);
        if (timeCount == 0)
        {
            startAgent();
        }
    }
}
