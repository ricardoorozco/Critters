using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    private bool isAlive = true;
    [SerializeField] private GameObject bloodSplashNpc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isAlive) {
            isAlive = false;
            GameObject.FindGameObjectWithTag("Stage1Controller").GetComponent<Stage1Controller>().addKill();
            Instantiate(bloodSplashNpc, new Vector3(transform.position.x, 0.01f, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
