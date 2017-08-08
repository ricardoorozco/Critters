using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    //private variable to store a reference to the player game object
	[SerializeField] private GameObject player;
    //hight to change for help button
    [SerializeField] private float hight = 1.91f;
    //help time in seconds
    [SerializeField] private int helpTime = 5;

    // LateUpdate is called after Update each frame
    void LateUpdate () 
	{
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
	}

    public void startHelpCam() {
        StartCoroutine("HelpCam");
    }

    IEnumerator HelpCam() {
        transform.position = new Vector3(player.transform.position.x, hight+10, player.transform.position.z);
        yield return new WaitForSeconds(helpTime);
        transform.position = new Vector3(player.transform.position.x, hight, player.transform.position.z);
        
    }
}
