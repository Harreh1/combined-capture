
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ravenTextManager2 :MonoBehaviour {

    public bool ravenCaptured = false;
    public bool elephantCaptured = false;
    public bool mouseCaptured = false;
    public bool cowCaptured = false;
    public bool turtleCaptured = false;
    public bool bombCaptured = false;
    public bool crocCaptured = false;

    public string animalName;
    public Text text;
    public Text scoreChange;

	// Use this for initialization
	void Start () {
        ravenCaptured = captureDetector.isRavenCaptured;
        elephantCaptured = captureDetector.isElephantCaptured;
        mouseCaptured = captureDetector.isMouseCaptured;
        cowCaptured = captureDetector.isCowCaptured;
        turtleCaptured = captureDetector.isTurtleCaptured;
        bombCaptured = captureDetector.isBombCaptured;
        crocCaptured = captureDetector.isCrocCaptured;
	}
	
	// Update is called once per frame
	void Update () {
        ravenCaptured = captureDetector.isRavenCaptured;
        elephantCaptured = captureDetector.isElephantCaptured;
        mouseCaptured = captureDetector.isMouseCaptured;
        cowCaptured = captureDetector.isCowCaptured;
        turtleCaptured = captureDetector.isTurtleCaptured;
        bombCaptured = captureDetector.isBombCaptured;
        crocCaptured = captureDetector.isCrocCaptured;

        if (elephantCaptured == true)
        {
            text.text = "Elephant Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+50";
            StartCoroutine(LateCall());
            captureDetector.isElephantCaptured = false;
        }
        else if (ravenCaptured == true)
        {
            text.text = "Raven Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+15";
            StartCoroutine(LateCall());
            captureDetector.isRavenCaptured = false;
        }
        else if (mouseCaptured == true)
        {
            text.text = "Mouse Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+5";
            StartCoroutine(LateCall());
            captureDetector.isMouseCaptured = false;
        }
        else if (cowCaptured == true)
        {
            text.text = "Cow Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+50";
            StartCoroutine(LateCall());
            captureDetector.isCowCaptured = false;
        }
        else if (turtleCaptured == true)
        {
            text.text = "Turtle Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+5";
            StartCoroutine(LateCall());
            captureDetector.isTurtleCaptured = false;
        }
        else if (crocCaptured == true)
        {
            text.text = "Crocodile Captured!";
            scoreChange.color = new Color(0, 255, 0);
            scoreChange.text = "+15";
            StartCoroutine(LateCall());
            captureDetector.isCrocCaptured = false;
        }
        else if (bombCaptured == true)
        {
            text.text = "Spider Captured!";
            scoreChange.color = new Color(255, 0, 0);
            scoreChange.text = "-20";
            StartCoroutine(LateCall());
            captureDetector.isBombCaptured = false;
        }

    }

    //after same sec Object to false
    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(2f);
        text.text = "";
        scoreChange.text = "";
    }


}
