using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

	// Use this for initialization
	void Start () {
        waitTime = startWaitTime;

        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots.position) < 0.5f){
            if(waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
            
            Debug.Log("YAY");
            moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
