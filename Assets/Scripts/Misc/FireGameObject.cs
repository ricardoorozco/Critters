using UnityEngine;

public class FireGameObject : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] bulletSpawn;
    public string firePointTag = "";
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == firePointTag)
        {
            Fire();
        }
    }

    public void Fire()
    {
        int pos = Random.Range(0,bulletSpawn.Length);

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn[pos].position,
            bulletSpawn[pos].rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}