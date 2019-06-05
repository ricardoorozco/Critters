using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTarget : MonoBehaviour {

	[SerializeField] private GameObject target;
	[SerializeField] private GameObject other;
    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;


    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.activeSelf)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, target.transform.position, fracJourney);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            if (target.transform.position != transform.position)
            {
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
			}
			if (tag == "Victim") {
				other.GetComponent<Animator>().SetFloat("Speed", 1);
			}
			if (tag == "Agent") {
				other.GetComponent<Animator>().SetFloat("velocity", 1);
			}
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, target.transform.position);
    }

    public string getNameTarget()
    {
        return this.target.name;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
