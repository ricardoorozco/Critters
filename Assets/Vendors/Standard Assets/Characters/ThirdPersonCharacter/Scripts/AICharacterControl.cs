using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        [SerializeField] private Transform[] targets;
        [SerializeField] int targetPos = 0;
		[SerializeField] private float offset;
		[SerializeField] private bool loop = true;

		[SerializeField] private Transform player;
		[SerializeField] private bool getAway = false;
		[SerializeField] private Transform getAwayPoint;
		[SerializeField] GameObject eyes;
		[SerializeField] float viewRange = 1.5f;

		private bool finish = false;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            if (target == null)
            {
                target = targets[0];
            }

            agent.updateRotation = false;
	        agent.updatePosition = true;

			player = GameObject.FindGameObjectWithTag ("Player").transform;
        }


        private void Update()
		{
			if (player == null) {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
				if (player == null) {
					return;
				}
			}

			if (getAway) {

				eyes.transform.LookAt(player);

				bool iSeeYou = seePlayer ();

				if (iSeeYou) {
					agent.speed = 0.6f;
					RunFrom ();
				} else if(target.position != getAwayPoint.position) {
					agent.speed = 0.3f;
				}
			}
			if (target != null && target.gameObject.activeSelf) {
				Vector3 distance = target.position - transform.position;


				if (distance.magnitude <= offset) {

					targetPos++;

					if (targetPos >= targets.Length) {
						
						targetPos = 0;

						if(!loop){
							finish = true;
						}
					}

					if (!finish) {
						target = targets [targetPos];
					}
				}
				agent.SetDestination (target.position);

				if (agent.remainingDistance > agent.stoppingDistance)
					character.Move (agent.desiredVelocity, false, false);
				else
					character.Move (Vector3.zero, false, false);
			}
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
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
			target = getAwayPoint;
		}
    }
}
