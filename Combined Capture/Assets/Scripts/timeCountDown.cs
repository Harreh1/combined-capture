﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCountDown :MonoBehaviour {

    public Text text;
    private float startTime;
    private float sceneTime;
    public Transform raven;
    public Transform elephant;
    public static string endTime;
    public static float endSeconds;
	// Use this for initialization
	void Start () {
        startTime = 59f;
        sceneTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = startTime - (Time.time-sceneTime);

        string seconds = (currentTime % 60).ToString("f2");

        text.text = seconds;
        endTime = (currentTime % 60).ToString("f0");

    }

}
