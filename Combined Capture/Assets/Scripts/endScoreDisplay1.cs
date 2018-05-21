using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endScoreDisplay1 : MonoBehaviour {

    public Text text;
	// Use this for initialization
	void Start () {
        text.text = "" + scoreTracker.score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
