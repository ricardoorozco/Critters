using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowMedals : MonoBehaviour {

	[SerializeField] Image trofyBotton;

	void Update(){
		trofyBotton.color = Social.localUser.authenticated && Social.localUser.state.ToString() != "Offline" ? Color.white : new Color (1f, 1f, 1f, 0.5f);
	}

	// Use this for initialization
	public void seeTrofys () {
		
	}

	private IEnumerator KeepCheckingAvatar()
	{
		float secondsOfTrying = 10;
		float secondsPerAttempt = 0.2f;
		while (secondsOfTrying > 0)
		{
			if (Social.localUser.image != null)
			{
				Debug.Log(Social.localUser.image);
				break;
			}

			secondsOfTrying -= secondsPerAttempt;
			yield return new WaitForSeconds(secondsPerAttempt);
		}
	}
}
