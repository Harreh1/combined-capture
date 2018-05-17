
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ravenTextManager2 :MonoBehaviour {

    public bool ravenCaptured = false;
    public bool elephantCaptured = false;
    public string animalName;
    public Text text;

	// Use this for initialization
	void Start () {
        ravenCaptured = captureDetector.isRavenCaptured;
        elephantCaptured = captureDetector.isElephantCaptured;
	}
	
	// Update is called once per frame
	void Update () {
        ravenCaptured = captureDetector.isRavenCaptured;
        elephantCaptured = captureDetector.isElephantCaptured;
        if (elephantCaptured == true)
        {
            text.text = "Elephant Captured!";
            StartCoroutine(LateCall());
            captureDetector.isElephantCaptured = false;
        }
        else if (ravenCaptured == true)
        {
            text.text = "Raven Captured!";
            StartCoroutine(LateCall());
            captureDetector.isRavenCaptured = false;
        }
        
	}

    //after same sec Object to false
    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(2f);
        text.text = "";
    }
}
