using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endGameDetection : MonoBehaviour {

    public Text ravenText;
    public Text turtleText;
    public Text blueTurtleText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ravenText == null && turtleText == null && blueTurtleText == null)
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
	}
}
