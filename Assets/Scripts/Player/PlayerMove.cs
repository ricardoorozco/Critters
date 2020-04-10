using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float speed = 1000f;
    [SerializeField] private float speedAuto = 0.3f;
    [SerializeField] private bool autoRunZ = false;
    [SerializeField] private bool intro = false;
    [SerializeField] private float limitXs = 0;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject scorch;
    [SerializeField] private GameObject explosionLight;
    [SerializeField] private bool canControl = true;
    [SerializeField] private bool deadDestroy = false;

    private void Start()
    {
        if (intro)
        {
            GetComponent<Animator>().SetBool("run", true);
            if (tag != "Palyer")
            {
                GetComponent<Animator>().speed *= Random.Range(0.1f, 1f);
            }
        }

    }

    private void FixedUpdate()
    {

        Vector3 dir = Vector3.zero;
        Vector3 position = Input.acceleration;

        position = Quaternion.Euler(120, 0, 0) * position;

        dir.x = position.x;
        dir.z = position.z;
        if (dir.x == 0 && dir.z == 0)
        {
            dir.x = Input.GetAxis("Horizontal") * 0.3f;
            dir.z = Input.GetAxis("Vertical") * 0.3f;
        }

        if (autoRunZ)
        {
            dir.z = 1;

            if (limitXs > 0)
            {
                if (dir.x > 0.1)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(limitXs, transform.position.y, transform.position.z + speedAuto), speedAuto * 0.2f);
                }
                if (dir.x < -0.1)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(-limitXs, transform.position.y, transform.position.z + speedAuto), speedAuto * 0.2f);
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedAuto);
                if (intro)
                {

                }
                else
                {
                    transform.Rotate(speed, 0, 0);
                }

            }
        }
        else if (canControl)
        {

            // clamp acceleration vector to the unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();
            if (GetComponent<Rigidbody>() != null)
                GetComponent<Rigidbody>().velocity = dir * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(scorch, new Vector3(transform.position.x, 0.01f, transform.position.z), Quaternion.identity);
            Instantiate(explosionLight, transform.position, transform.rotation);
            if (deadDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
