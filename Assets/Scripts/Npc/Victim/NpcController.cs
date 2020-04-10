using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour {

	[SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgentNpc;
	[SerializeField] private Transform targetFollow;
	[SerializeField] private Transform[] targets;
	[SerializeField] int targetPos = 0;
    [SerializeField] private float offset;

	[SerializeField] private Transform player;
	[SerializeField] private bool getAway = false;
	[SerializeField] GameObject eyes;
	[SerializeField] float viewRange = 2f;

    // Use this for initialization
    void Start () {
		if (navMeshAgentNpc == null) {
			navMeshAgentNpc = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		}
		if (targetFollow == null) {
			targetFollow = targets[0];
		}
		navMeshAgentNpc.SetDestination (targetFollow.position);
	}
	
	// Update is called once per frame
	void Update () {

		bool iSeeYou = seePlayer ();

		if (iSeeYou && getAway) {
			RunFrom ();
			return;
		}

		Vector3 distance = targetFollow.position - transform.position;

		if(distance.magnitude <= offset){
			targetPos++;
			if (targetPos >= targets.Length) {
				targetPos = 0;
			}
			targetFollow = targets [targetPos];
		}
		navMeshAgentNpc.SetDestination (targetFollow.position);
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

	public void RunFrom()
	{

		// store the starting transform
		Transform startTransform = transform;

		//temporarily point the object to look away from the player
		transform.rotation = Quaternion.LookRotation(transform.position - player.position);

		//Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
		// for this if you want variable results) and store it in a new Vector3 called runTo
		Vector3 runTo = transform.position + transform.forward * 3;

		//So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

		UnityEngine.AI.NavMeshHit hit;    // stores the output in a variable called hit

		// 5 is the distance to check, assumes you use default for the NavMesh Layer name
		UnityEngine.AI.NavMesh.SamplePosition(runTo, out hit, 5, 1 << UnityEngine.AI.NavMesh.GetNavMeshLayerFromName("Default"));  

		// reset the transform back to our start transform
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;

		// And get it to head towards the found NavMesh position
		navMeshAgentNpc.SetDestination(hit.position);
	}
}