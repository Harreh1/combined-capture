using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blueTurtleTextManager : MonoBehaviour {

    public bool isCaptured = false;
    public string animalName;
    public Text text;

    // Use this for initialization
    void Start()
    {

        isCaptured = EnemyController.isCaptured;
    }

    // Update is called once per frame
    void Update()
    {
        isCaptured = EnemyController.isCaptured;
        animalName = EnemyController.animalName;
        if (isCaptured == true && animalName == "Turtle_blue")
        {
            text.text = "Blue Turtle Captured!";
            Destroy(gameObject, 2f);
        }
    }
}
