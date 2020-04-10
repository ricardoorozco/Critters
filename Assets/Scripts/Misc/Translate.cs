using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour {

	[SerializeField] bool x;
	[SerializeField] bool y;
	[SerializeField] bool z;

	[SerializeField] float speed;

	// Update is called once per frame
	void Update () {
		if (x)
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		if (y)
		{
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}
		if (z)
		{
			transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
		}
	}
}
