using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {



	[SerializeField] private GameObject player;       //Public variable to store a reference to the player game object

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		transform.position = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
	}
}
