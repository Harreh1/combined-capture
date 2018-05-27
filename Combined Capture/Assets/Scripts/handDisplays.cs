using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handDisplays : MonoBehaviour {

    public Sprite handRight;
    public Sprite handLeft;
    public Sprite handOpen;
    public Sprite handClosed;
    public SpriteRenderer sp;
    public float startTime;
    public float ctime;
    public static float dtime;

    public bool hitLeft;
    public bool hitRight;
    // Use this for initialization
    void Start () {
        sp = GetComponent<Renderer>() as SpriteRenderer;
        sp.enabled = false;
        startTime = Time.time;
        ctime = 0;
        hitRight = true;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] detections;
        detections = GameObject.FindGameObjectsWithTag("bodyPos");
        Quaternion r = this.transform.rotation;
        //Debug.Log(r.z);
        if(hitRight == true)
        {
            r.z += 0.015f;
            if (r.z > 0.25)
            {
                hitRight = false;
            }
        }
        else
        {
            r.z -= 0.015f;
            if (r.z < -0.25)
            {
                hitRight = true;
            }
        }

        this.transform.rotation = r;
        if (detections.Length < 500)
        {
            if(ctime == 0)
            {
                ctime = Time.time - startTime;
                dtime = 0;
            }
        } else
        {
            ctime = 0;
            sp.enabled = false;

        }
        if(ctime != 0)
        {
            if (Time.time - ctime > 10)
            {
                sp.enabled = true;
            }
        }
    }

}
