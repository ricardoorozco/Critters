using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
    
    Canvas canvas;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
		if (canvas != null) {
			canvas.gameObject.SetActive (false);
		}
    }
    
    public void Pause()
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
}