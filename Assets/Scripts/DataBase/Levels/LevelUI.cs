using UnityEngine;

public class LevelUI : MonoBehaviour {

	LevelDB conn;
	ProfileDB profileDb;

	[SerializeField] private int level;

	private int id;
	private int active;
	private int best_time;
	private int best_stars;
	private int profile;

	[SerializeField] GameObject locked;
	[SerializeField] Sprite start;
	[SerializeField] Sprite startless;

	[SerializeField] SpriteRenderer start1;
	[SerializeField] SpriteRenderer start2;
    [SerializeField] SpriteRenderer start3;
    
    [SerializeField] TextMesh bestTime;

    // Use this for initialization
    void Start() {

        conn = new LevelDB();
        profileDb = new ProfileDB();

        int currentUser = profileDb.getCurrentUserId();

        int[] data = new int[6];

        if (currentUser > 0) {
            data = conn.getStatusLevel(level, currentUser);
        } else {
            return;
        }

        id = data[0];
        level = data[1];
        active = data[2];
        best_time = data[3];
        best_stars = data[4];
        profile = data[5];

        if (level == 1) {
            active = 1;
            GetComponentInParent<SetTargetMesh>().setEnableStatus(true);
        } else {
            int[] prelvl = conn.getStatusLevel(level - 1, currentUser);
            if (prelvl[4] > 0) {
                active = 1;
                GetComponentInParent<SetTargetMesh>().setEnableStatus(true);
            }
        }

        if (active != 1) {
            locked.SetActive(true);
        } else {
            locked.SetActive(false);
        }

        start1.sprite = (best_stars > 0) ? start : startless;
        start2.sprite = (best_stars > 1) ? start : startless;
        start3.sprite = (best_stars > 2) ? start : startless;

        string[] recordTimeLevel = new string[1];
        recordTimeLevel = conn.getBestStatusLevel(level, currentUser);

        if (recordTimeLevel[0] != null && int.Parse(recordTimeLevel[0]) > 0) {

            float m = Mathf.Floor(float.Parse(recordTimeLevel[0]) / 60);
            float s = Mathf.Floor(float.Parse(recordTimeLevel[0]) - (m * 60));

            bestTime.text = m.ToString("00") + ":" + s.ToString("00");
        }

    }
}
