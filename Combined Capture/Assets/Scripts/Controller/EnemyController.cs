using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    private float waitTime;
    public float startWaitTime;

    private bool avoiding;

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

        if (!avoiding)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
            Vector2 direction = moveSpots.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 3f * Time.deltaTime);
        }
        
        if (Vector2.Distance(transform.position, moveSpots.position) < 0.5f){
            if(waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }

        sensors();
	}

    [Header("Sensors")]
    public float sideSensorPos = 1300f;
    public float frontSensorAngle = 30;

    private void sensors()
    {

        // Vector 2 of the moveSpot
        Vector2 moveSpotsPos = new Vector2(moveSpots.position.x, moveSpots.position.y);

        float avoidMultiplier = 0;
        avoiding = false;

        //Front Sensor
        Vector2 sensorStartPos = transform.position;
        RaycastHit2D frontSensor = Physics2D.Raycast(sensorStartPos, (moveSpotsPos - sensorStartPos).normalized, 2500);

        if (frontSensor.collider != null && frontSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(sensorStartPos, frontSensor.point);
            avoidMultiplier += 5000f;

        }

        //Front right sensor
        Vector2 rightSensorPos = transform.position + transform.forward + transform.up + transform.right * sideSensorPos;
        RaycastHit2D rightSensor = Physics2D.Raycast(rightSensorPos, (moveSpotsPos - rightSensorPos).normalized, 2500);
        //Front left sensor 
        Vector2 leftSensorPos = transform.position + transform.forward + transform.up - transform.right * sideSensorPos;
        RaycastHit2D leftSensor = Physics2D.Raycast(leftSensorPos, (moveSpotsPos - leftSensorPos).normalized, 2500);
        if (rightSensor.collider != null && rightSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(rightSensorPos, rightSensor.point);
            avoiding = true;
            avoidMultiplier += 1000f;
        }
        else if (leftSensor.collider != null && leftSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(leftSensorPos, leftSensor.point);
            avoiding = true;
            avoidMultiplier += 1000f;
        }
        
        if (avoiding)
        {
            Vector2 direction = moveSpots.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle + avoidMultiplier, Vector3.forward);
            Quaternion.Slerp(transform.rotation, rotation, 3f * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, moveSpots.position * avoidMultiplier, speed * Time.deltaTime);
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yay");
    }


}
