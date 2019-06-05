using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObjects : MonoBehaviour {

    [SerializeField] private GameObject[] objects;
    [SerializeField] private float timeDelay = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if(objects.Length > 0)
        {
            StartCoroutine("displayTheObjects");
        }
    }

    IEnumerator displayTheObjects()
    {
        yield return new WaitForSeconds(timeDelay);
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }
}
