using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaunchMessage : MonoBehaviour
{

    [SerializeField] Canvas canvas;
    [SerializeField] bool pauseGame = true;
    [SerializeField] bool useModelTriggerAnimation = false;
    [SerializeField] GameObject model;
    [SerializeField] string animationName;
    [SerializeField] bool autoClose = false;
    [SerializeField] int timeToClose = 0;

    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    public void Launch()
    {
        if (canvas.gameObject.activeSelf) {
            return;
        }

        canvas.gameObject.SetActive(true);
        if (pauseGame)
        {
            Time.timeScale = 0;
        }
        if (useModelTriggerAnimation)
        {
            if(model != null)
            {
                model.GetComponent<Animator>().SetTrigger(animationName);
            }
        }
        if (autoClose)
        {
            StartCoroutine("autoCloseStart");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Launch();
        }
    }

    IEnumerator autoCloseStart()
    {
        yield return new WaitForSeconds(timeToClose);
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}