
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endScreenReturn :MonoBehaviour {

    public Text text;
    private float startTime;
    private float sceneTime;
    public Transform raven;
    public Transform elephant;
    public static string endTime;
    public static float endSeconds;
	// Use this for initialization
	void Start () {
        startTime = 15f;
        sceneTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = startTime - (Time.time-sceneTime);

        string seconds = (currentTime % 60).ToString("f0");

        text.text = "Returning to main menu in: " + seconds;
        if (seconds == "0")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

    }

}
