using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class animalCounter : MonoBehaviour {

    public int counter;
    public Text text;
	// Use this for initialization
	void Start () {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        counter = 0;
        GameObject[] animals;
        animals = GameObject.FindGameObjectsWithTag("animal");
        foreach (GameObject d in animals)
        {
            counter++;
        }
        text.text = "Animals: " + counter;

        if (counter >= 10)
        {
            scoreTracker.time = timerManager.endTime;
            SceneManager.LoadScene("game over", LoadSceneMode.Single);
        }
    }
}
