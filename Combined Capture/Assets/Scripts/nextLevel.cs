using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {
    public int counter = 0;
	// Use this for initialization
	void Start () {
		
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
        if(counter == 0)
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
}
