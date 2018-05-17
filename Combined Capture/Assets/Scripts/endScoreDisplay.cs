using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endScoreDisplay : MonoBehaviour {

    public Text text;
	// Use this for initialization
	void Start () {
        text.text = "You lasted " + timerManager.endTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
