using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMapController : MonoBehaviour {

	ProfileDB profileDb;
	[SerializeField]Text userName;


	public void Start()
	{
		profileDb = new ProfileDB();
		userName.text = profileDb.getCurrentUserName ();
	}
}
