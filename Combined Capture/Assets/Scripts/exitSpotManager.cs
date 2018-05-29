using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitSpotManager : MonoBehaviour {

    public static Transform[] exitSpots;
    public static Vector3 returnPoint;
	// Use this for initialization
	void Start () {
        returnPoint = new Vector3(-15, 3, -1.9f);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public static Vector3 getRandomExitpointPos()
    {
        
        return returnPoint;
    }
}
