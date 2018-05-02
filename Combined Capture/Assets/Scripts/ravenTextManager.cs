
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ravenTextManager :MonoBehaviour {

    public bool isCaptured = false;
    public string animalName;
    public Text text;

	// Use this for initialization
	void Start () {
        
        isCaptured = EnemyController.isCaptured;
	}
	
	// Update is called once per frame
	void Update () {
        isCaptured = EnemyController.isCaptured;
        animalName = EnemyController.animalName;
        if (isCaptured == true && animalName == "Raven")
        {
            text.text = "Raven Captured!";
            Destroy(gameObject, 2f);
        }	
	}

}
