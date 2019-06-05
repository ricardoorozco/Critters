using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    //private variable to store a reference to the player game object
	[SerializeField] private GameObject player;
    //for stage2 offset back to player
	[SerializeField] private float offsetZ = 0;

    //limits
    [SerializeField] private bool useLimit = false;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    
    void Update () 
	{
		if (player != null) {
			transform.position = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z - offsetZ);
            
            if (useLimit)
            {
                if (transform.position.z > upLimit)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, upLimit);
                }
            }
            if (useLimit)
            {
                if (transform.position.z < downLimit)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, downLimit);
                }
            }
            if (useLimit)
            {
                if (transform.position.x < leftLimit)
                {
                    transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
                }
            }
            if (useLimit)
            {
                if (transform.position.x > rightLimit)
                {
                    transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
                }
            }
        }
        
	}
}
