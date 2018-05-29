using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handDisplaysClosed : MonoBehaviour {

    public Sprite handRight;
    public Sprite handLeft;
    public Sprite handOpen;
    public Sprite handClosed;
    public SpriteRenderer sp;
    public float startTime;
    public float ctime;
    public static float dtime;

    // Use this for initialization
    void Start () {
        sp = GetComponent<Renderer>() as SpriteRenderer;
        sp.enabled = false;
        startTime = Time.time;
        ctime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] detections1;
        detections1 = GameObject.FindGameObjectsWithTag("bodyPos");
        GameObject[] detections;
        detections = GameObject.FindGameObjectsWithTag("circlePos");
        Quaternion r = this.transform.rotation;
        if (detections1.Length > 100)
        {
            if (detections.Length < 50)
            {
                if (ctime == 0)
                {
                    ctime = Time.time - startTime;
                    dtime = 0;
                }
            }
            else
            {
                ctime = 0;
                sp.enabled = false;

            }
            if (ctime != 0)
            {
                if (Time.time - ctime > 6)
                {
                    sp.enabled = true;
                }
            }
        } else
        {
            sp.enabled = false;
        }

    }

}
