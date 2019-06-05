using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    [SerializeField] private ArrayList elementsTracks;
    [SerializeField] private GameObject[] elementsTrack9;
    [SerializeField] private GameObject[] elementsTrack18;
    [SerializeField] private GameObject[] elementsTrack27;
    [SerializeField] private int trackLength;
    [SerializeField] private float advantage;

    [SerializeField] private GameObject[] obstacles;

    // Use this for initialization
    void Start () {
        int i = 0;
        int totalLength = 0;

        while (totalLength < trackLength){
            int elementTrackLength = Random.Range(1, 4);
            
            if (elementTrackLength == 1)
            {
                Instantiate(elementsTrack9[Random.Range(0, elementsTrack9.Length)], new Vector3(0, 0, totalLength), Quaternion.identity);
                if (totalLength > trackLength * advantage) {
                    Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(0, obstacles.Length) > obstacles.Length * 0.5 ? 1 : -1, 0, totalLength), Quaternion.identity);
                }
                totalLength += 9;
            }
            else if (elementTrackLength ==2)
            {
                Instantiate(elementsTrack18[Random.Range(0, elementsTrack18.Length)], new Vector3(0, 0, totalLength), Quaternion.identity);
                if (totalLength > trackLength * advantage)
                {
                    Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(0, obstacles.Length) > obstacles.Length * 0.5 ? 1 : -1, 0, totalLength), Quaternion.identity);
                }
                totalLength += 18;
            }
            else
            {
                Instantiate(elementsTrack27[Random.Range(0, elementsTrack27.Length)], new Vector3(0, 0, totalLength), Quaternion.identity);
                if (totalLength > trackLength * advantage)
                {
                    Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(0, obstacles.Length) > obstacles.Length * 0.5 ? 1 : -1, 0, totalLength), Quaternion.identity);
                }
                totalLength += 27;
            }
            i++;
        }
    }
}
