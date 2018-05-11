
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ravenTextManager2 :MonoBehaviour {

    public bool isCaptured = false;
    public string animalName;
    public Text text;

	// Use this for initialization
	void Start () {
        isCaptured = ravenController.isCaptured;
	}
	
	// Update is called once per frame
	void Update () {
        isCaptured = ravenController.isCaptured;
        if (isCaptured == true)
        {
            text.text = "Raven Captured!";
            Destroy(gameObject, 3f);
        }	
	}

}
