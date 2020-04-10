using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

	[SerializeField] Animator animator;

	[SerializeField] GameObject eyes;
	[SerializeField] float viewRange = 2f;

	[SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgentNpc;
	[SerializeField] private Transform target;

	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Transform bulletSpawn;
	[SerializeField] private float speedBullet = 10f;

	[SerializeField] private bool watchman = false;

    // Use this for initialization
    void Start () {
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}
		if (navMeshAgentNpc == null) {
			navMeshAgentNpc = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		}
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {

            eyes.transform.LookAt(target);
            bulletSpawn.LookAt(target);

            bool iSeeYou = seePlayer ();

			if (iSeeYou) {
				transform.LookAt (target);
				animator.SetTrigger ("shoot");
				if (navMeshAgentNpc != null && !watchman) {
					navMeshAgentNpc.isStopped = true;
				}
				Fire();
			} else {
				if (navMeshAgentNpc != null && !watchman) {
					navMeshAgentNpc.isStopped = false;
				}
			}

			if (navMeshAgentNpc != null && !watchman) {
				if (navMeshAgentNpc.remainingDistance > navMeshAgentNpc.stoppingDistance && !iSeeYou) {
					animator.SetFloat ("velocity", 1);
				} else {
					animator.SetFloat ("velocity", 0);
				}

				navMeshAgentNpc.SetDestination (target.position);
			}else{
				animator.SetFloat ("velocity", 0);
			}

		} else {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	bool seePlayer(){
		
		RaycastHit hit;

		bool iSeeYou = false;

		if (Physics.Raycast (eyes.transform.position, eyes.transform.forward, out hit,viewRange)) {

			if (hit.transform.tag == "Player") {
				iSeeYou = true;
			} 
		}

		return iSeeYou;
	}

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speedBullet;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 0.2f);
    }
}
