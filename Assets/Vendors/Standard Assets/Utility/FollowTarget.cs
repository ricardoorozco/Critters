using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
	public class FollowTarget : MonoBehaviour
	{
		public Transform target;
		public Vector3 offset = new Vector3(0f, 7.5f, 0f);
		//follow
		public bool x = true,y = true,z = true;

		private void LateUpdate()
		{
			if (target != null) {
				transform.position = new Vector3 (x ? target.position.x : transform.position.x, y ? target.position.y : transform.position.y, z ? target.position.z : transform.position.z) + offset;
			}
		}
	}
}