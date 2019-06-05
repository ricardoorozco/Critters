using UnityEngine;

public class StageUI : MonoBehaviour
{

    LevelDB conn;
    ProfileDB profileDb;

    [SerializeField] private int stage;
    [SerializeField] private int lastLvl;
    private int active;

    // Use this for initialization
    void Start()
    {

        conn = new LevelDB();
        profileDb = new ProfileDB();

        int currentUser = profileDb.getCurrentUserId();
        
        int[] prelvl = conn.getStatusLevel(lastLvl, currentUser);

        if (prelvl[4] > 0)
        {
            active = 1;
            GetComponentInParent<SetTargetMesh>().setEnableStatus(true);
        }

        if (active != 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
