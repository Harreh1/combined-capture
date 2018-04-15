using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yay");
    }

}
