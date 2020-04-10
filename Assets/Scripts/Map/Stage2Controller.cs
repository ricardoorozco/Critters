using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Stage2Controller : MonoBehaviour {
	#if UNITY_ANDROID
	private string gameId = "1578407";
	#elif UNITY_IOS
	private string gameId = "";
	#elif UNITY_EDITOR
	private string gameId = "";
	#endif

    [SerializeField] private GameObject player;
    [SerializeField] private bool playerIsDeath = false;

    [SerializeField] private Canvas canvas;
	[SerializeField] private Canvas canvasBlood;


	[SerializeField] GameObject revive;
    // Use this for initialization
    void Start ()
    {
        canvas.gameObject.SetActive(false);
        canvasBlood.gameObject.SetActive(false);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			if (!player.activeSelf) {
				if (playerIsDeath) {
					return;
				}
				playerIsDeath = true;
				canvasBlood.gameObject.SetActive (true);
				StartCoroutine ("stopGame");
			}
		}
    }

    IEnumerator stopGame()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        canvas.gameObject.SetActive(true);
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
        canvas.gameObject.SetActive(false);
        canvasBlood.gameObject.SetActive(false);
        Time.timeScale = 1;
        playerIsDeath = false;
        player.SetActive(true);
    }
}
