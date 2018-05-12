
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager :MonoBehaviour {

    public Text text;
    private float startTime;
    public Transform raven;
    public Transform bug;

	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnRaven", 5f, 4f);
        InvokeRepeating("spawnBug", 12f, 10f);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time - startTime;

        string minutes = ((int)currentTime / 60).ToString();
        string seconds = (currentTime % 60).ToString("f2");
        if((currentTime % 60) < 10)
        {
            text.text = minutes + ":0" + seconds;
        }
        else
        {
            text.text = minutes + ":" + seconds;
        }

    }

    private void spawnRaven()
    {
        Instantiate(raven, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
    private void spawnBug()
    {
        Instantiate(bug, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
}
