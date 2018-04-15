using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    private float waitTime;
    public float startWaitTime;

    private bool avoiding;
    private float avoidingMultipler;

    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

	// Use this for initialization
	void Start () {
        waitTime = startWaitTime;
        avoidingMultipler = 0;
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
    public float sideSensorPos;
    public float frontSensorAngle = 30;
    

    private void sensors()
    {

        // Vector 2 of the moveSpot
        Vector2 moveSpotsPos = new Vector2(moveSpots.position.x, moveSpots.position.y);
        //Length of sesnor line
        float sensorLength = 3500;

        if (!avoiding){ avoidingMultipler = 0;}
        avoiding = false;

        //Front Sensor
        Vector2 frontSensorPos = transform.position;
        Vector2 frontSensorAim = transform.position + transform.up * sensorLength;
        RaycastHit2D frontSensor = Physics2D.Raycast(frontSensorPos, (frontSensorAim - frontSensorPos).normalized, 3500);
        if (frontSensor.collider != null && frontSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(frontSensorPos, frontSensor.point);
            //moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            avoiding = true;
            avoidingMultipler += 12000f;
        }

        //Front right sensor
        Vector2 rightSensorPos = transform.position + transform.up + transform.right * sideSensorPos;
        Vector2 rightSensorAim = transform.position + transform.up * sensorLength + transform.right * sideSensorPos;
        RaycastHit2D rightSensor = Physics2D.Raycast(rightSensorPos, (rightSensorAim - rightSensorPos).normalized, sensorLength);
        //Front left sensor 
        Vector2 leftSensorPos = transform.position + transform.forward + transform.up - transform.right * sideSensorPos;
        Vector2 leftSensorAim = transform.position + transform.up * sensorLength - transform.right * sideSensorPos;
        RaycastHit2D leftSensor = Physics2D.Raycast(leftSensorPos, (leftSensorAim - leftSensorPos).normalized, sensorLength);
        //Front-right
        if (rightSensor.collider != null && rightSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(rightSensorPos, rightSensor.point);
            avoiding = true;
            avoidingMultipler += 6000f;
            
        }
        //Front-left
        if (leftSensor.collider != null && leftSensor.collider.CompareTag("Coll"))
        {
            Debug.DrawLine(leftSensorPos, leftSensor.point);
            avoiding = true;
            avoidingMultipler += 6000f;
        }
        
        if (avoiding)
        {
            Vector2 direction = moveSpots.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle + avoidingMultipler, Vector3.forward);
            Quaternion.Slerp(transform.rotation, rotation, 3f * Time.deltaTime);
            moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            transform.position = Vector2.MoveTowards(transform.position, moveSpots.position * avoidingMultipler, speed * Time.deltaTime);
        }
        //Debug.DrawLine(leftSensorPos, leftSensorAim, Color.red);
        //Debug.DrawLine(rightSensorPos, rightSensorAim, Color.red);
        //Debug.DrawLine(frontSensorPos, frontSensorAim, Color.red);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yay");
    }


}
