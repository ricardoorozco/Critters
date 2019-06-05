using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePathIntro : MonoBehaviour {
    
    [SerializeField] GameObject nextPath;
    [SerializeField] float speed;
    [SerializeField] float timeWait;
    [SerializeField] string triggerAnimationWait;
    [SerializeField] string activeBoolAnimationName;
    [SerializeField] string floatAnimationName;
    [SerializeField] float floatAnimationValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<GoTarget>().getNameTarget() == gameObject.name)
        {
            StartCoroutine("changePath", other.gameObject);
		}
	}

	IEnumerator changePath(GameObject obj)
	{
		if (triggerAnimationWait != "")
		{
			obj.GetComponent<Animator>().SetTrigger(triggerAnimationWait);
		}
		if (activeBoolAnimationName != "")
		{
			obj.GetComponent<Animator>().SetBool(activeBoolAnimationName, true);
		}
		if (floatAnimationName != "")
		{
			obj.GetComponent<Animator>().SetFloat(floatAnimationName, floatAnimationValue);
		}
		yield return new WaitForSeconds(timeWait);
		if (nextPath != null)
		{
			obj.GetComponent<GoTarget>().setTarget(nextPath);
		}
		obj.GetComponent<GoTarget>().setSpeed(speed);
		obj.GetComponent<Animator>().SetBool("run", true);
	}
}
