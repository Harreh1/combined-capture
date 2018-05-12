
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ravenTextManager2 :MonoBehaviour {

    public bool isCaptured = false;
    public string animalName;
    public Text text;

	// Use this for initialization
	void Start () {
        isCaptured = ravenCaptureDetector.isCaptured;
	}
	
	// Update is called once per frame
	void Update () {
        isCaptured = ravenCaptureDetector.isCaptured;
        if (isCaptured == true)
        {
            text.text = "Raven Captured!";
            StartCoroutine(LateCall());
            ravenCaptureDetector.isCaptured = false;
        }	
	}

    //after same sec Object to false
    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(2f);
        text.text = "";
    }
}
