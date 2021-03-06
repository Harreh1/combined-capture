﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager :MonoBehaviour {

    public Text text;
    private float startTime;
    public Transform raven;
    public Transform elephant;
    public static string endTime;
    public static float endSeconds;
    public bool speedup = true;
    public bool speedup2 = true;
	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnRaven", 5f, 3f);
        InvokeRepeating("spawnElephant", 11f, 10f);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time - startTime;

        string minutes = ((int)currentTime / 60).ToString();
        string seconds = (currentTime % 60).ToString("f2");
        if((currentTime%60).ToString("f0") == "59" && speedup)
        {
            InvokeRepeating("spawnRaven", 1f, 3f);
            Debug.Log("worked");
            speedup = false;
        }
        if ((currentTime % 120).ToString("f0") == "119" && speedup2)
        {
            InvokeRepeating("spawnRaven", 1f, 1f);
            Debug.Log("worked");
            speedup2 = false;
        }
        if ((currentTime % 60) < 10)
        {
            text.text = minutes + ":0" + seconds;
            endTime = minutes + ":0" + seconds;
        }
        else
        {
            text.text = minutes + ":" + seconds;
            endTime = minutes + ":" + seconds;
        }
        endSeconds = currentTime;


    }

    private void spawnRaven()
    {
        Instantiate(raven, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
    private void spawnElephant()
    {
        Instantiate(elephant, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
}
