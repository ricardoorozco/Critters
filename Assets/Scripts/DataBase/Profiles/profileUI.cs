using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class profileUI : MonoBehaviour
{

	[SerializeField] InputField usernameText;
	[SerializeField] RectTransform listProfilesPanel;
	[SerializeField] GameObject prefabProfileButton;
	float topContentProfiles = 0f;
	float addSizePrefab = 50f;
	float spacing = 5f;
	float offset;

	[SerializeField] bool isSetProfileBottom = false;

    ProfileDB conn;
    [SerializeField] private string nextSceneName;
    
    private void Start()
	{
		if (isSetProfileBottom) {
			return;
		}

        conn = new ProfileDB();

        string currentUserName = conn.getCurrentUserName();

        if(currentUserName != "")
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }

        listProfiles ();
	}

	public void setCurrentProfile()
	{
		string username = gameObject.name;

        conn = new ProfileDB();
        conn.setCurrentUser (username);
	}

	public void createProfile()
	{
		if (usernameText.text != "")
		{
            DateTime fecha = DateTime.Now;
			conn.createProfile(
                fecha.Year.ToString("00") + 
                fecha.Month.ToString("00") + 
                fecha.Day.ToString("00") + 
                fecha.Hour.ToString("00") + 
                fecha.Minute.ToString("00") + 
                fecha.Second.ToString("00") + 
                fecha.Millisecond.ToString("000"),
                usernameText.text);
			usernameText.text = "";
		}

		for(int i = 0; i < listProfilesPanel.gameObject.transform.childCount; i++){
			Destroy (listProfilesPanel.gameObject.transform.GetChild (i).gameObject);
		}
		listProfiles ();
	}

	public void listProfiles()
	{
		List<String[]> profiles = conn.listProfiles();

		int count = profiles.Count;
		offset = addSizePrefab * count;
		offSetDown();

		int pos = 0;
		foreach (var profile in profiles)
		{
			resultSetElement(profile, profile[1], pos);
			pos++;
		}
	}

	public void resultSetElement(String[] profile, string name, int pos)
	{
		GameObject resultObject = Instantiate(prefabProfileButton) as GameObject;
		resultObject.transform.SetParent(listProfilesPanel.transform);
		resultObject.name = name;
		resultObject.GetComponentInChildren<Text>().text = name;

		RectTransform resultRectTransform = resultObject.GetComponent<RectTransform>();

		//clear position
		resultRectTransform.localScale = new Vector3(1f, 1f, 1f);
		resultRectTransform.anchorMin = new Vector2(0f, 0f);
		resultRectTransform.anchorMax = new Vector2(1f, 1f);
		resultRectTransform.pivot = new Vector2(0.5f, 0.5f);
		resultRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
		//set new position
		resultRectTransform.offsetMin = new Vector2(10f, offset - (pos * addSizePrefab) - addSizePrefab);
		resultRectTransform.offsetMax = new Vector2(-10f, -1 * (pos * addSizePrefab + spacing));
	}

	public void offSetDown()
	{
		if (offset < 300) {
			offset = 300;
		}
		listProfilesPanel.offsetMin = new Vector2(listProfilesPanel.offsetMin.x, offset * -1);
		listProfilesPanel.offsetMax = new Vector2(listProfilesPanel.offsetMax.x, listProfilesPanel.offsetMax.y);
	}
}
