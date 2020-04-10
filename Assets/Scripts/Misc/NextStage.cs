using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour {

	[SerializeField] private string nextSceneName;
	[SerializeField] private bool autoStart = false;

	void Start(){
		if (autoStart) {
			changeLevel ();
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			changeLevel ();
        }
    }

    public void changeLevel()
	{
		Time.timeScale = 1;
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
