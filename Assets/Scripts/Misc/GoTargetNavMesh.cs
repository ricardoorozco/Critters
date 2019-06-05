using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTargetNavMesh : MonoBehaviour {

	[SerializeField] private UnityEngine.AI.NavMeshAgent agent;
	[SerializeField] private Transform target;
	[SerializeField] private Transform[] targets;
	[SerializeField] private int targetPos = 0;
	[SerializeField] private float offset;
	[SerializeField] private bool loop = true;

	private bool finish = false;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	private void Update()
	{
		if (target != null && target.gameObject.activeSelf) {
			Vector3 distance = target.position - transform.position;

			if (distance.magnitude <= offset) {

				targetPos++;

				if (targetPos >= targets.Length) {

					targetPos = 0;

					if (!loop) {
						finish = true;
					}
				}

				if (!finish) {
					target = targets [targetPos];
				}
			}
			agent.SetDestination (target.position);
		} else if(targets.Length > 0){
			targetPos = 0;
			target = targets [targetPos];
		}
	}


	public void SetTarget(Transform target)
	{
		this.target = target;
	}

	public void SetTargets(Transform[] targets)
	{
		this.targets = targets;
		this.targetPos = 0;
	}
}
