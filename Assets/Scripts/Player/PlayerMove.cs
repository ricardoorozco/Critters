using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

	public float speed = 1000f;
	
	// Update is called once per frame
	private void Update () {

		Vector3 dir = Vector3.zero;

		// we assume that the device is held parallel to the ground
		// and the Home button is in the right hand

		// remap the device acceleration axis to game coordinates:
		// 1) XY plane of the device is mapped onto XZ plane
		// 2) rotated 90 degrees around Y axis

		Vector3 position = Input.acceleration;

        position = Quaternion.Euler (90, 0, 0) * position;

		dir.x = position.x;
		dir.z = position.z;

        /*dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");*/
        
        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move per frame...
		dir *= Time.deltaTime * speed;

		// Move object
		transform.Translate (dir, Space.World);
        //rote object
        transform.Rotate(new Vector3(
            transform.rotation.x + (1000f * dir.z), 
            transform.rotation.y, 
            transform.rotation.z + (1000f * -dir.x)
            ) * Time.deltaTime * 10f, Space.World);
    }
}
