using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

	public float speed = 1000.0f;
	
	// Update is called once per frame
	private void Update () {

		Vector3 dir = Vector3.zero;

		// we assume that the device is held parallel to the ground
		// and the Home button is in the right hand

		// remap the device acceleration axis to game coordinates:
		// 1) XY plane of the device is mapped onto XZ plane
		// 2) rotated 90 degrees around Y axis

		Vector3 rotation = Input.acceleration;

		rotation = Quaternion.Euler (90, 0, 0) * rotation;

		dir.x = rotation.x;
		dir.z = rotation.z;

		// clamp acceleration vector to the unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		// Move object
		transform.Translate (dir * speed, Space.World);
	}
}
