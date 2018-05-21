using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elephantController : MonoBehaviour {

    public float speed;
    private float waitTime;
    public float startWaitTime;

    private bool avoiding;
    private float avoidingMultipler;

    public Transform[] moveSpotsArray;
    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    List<Vector3> collisionPoints;

    public static string animalName;

    public Slider captureProgress;
    public GameObject canvas;
    public Slider currentSlider;

    public bool hit;
    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(true);
        waitTime = startWaitTime;
        avoidingMultipler = 0;
        moveSpots = moveSpotsArray[Random.Range(0, moveSpotsArray.Length-1)];
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        canvas = GameObject.Find("Canvas");
        currentSlider = Instantiate(captureProgress);
        currentSlider.transform.position = transform.position;
        currentSlider.transform.SetParent(canvas.transform);
        currentSlider.transform.localScale -= new Vector3(44.4f, 44.8f, 0);
        hit = false;
    }
	
	// Update is called once per frame
	void Update () {
        hit = false;
        Vector3 currentpos = transform.position;
        if (currentSlider.value == 0)
        {
            currentpos.y += 20f;
        }
        else
        {
            currentpos.y += 1.5f;
        }
        currentSlider.transform.position = currentpos;

        collisionPoints = DepthViewTest.circlePositions;
        if (DepthViewTest.circlePositions != null)
        {
            int pointChecker = 0;
            float width = 1.3f;
            foreach(var p in collisionPoints)
            {
                if (p.x > transform.position.x - 0.1 && p.x < transform.position.x + 0.1
                    && p.y > transform.position.y - 0.1 && p.y < transform.position.y + 0.1)
                {
                    pointChecker += 1;

                }

                if(pointChecker == 0)
                {
                    continue;
                }

                if (p.x > transform.position.x - width - 0.1 && p.x < transform.position.x - width + 0.1
                    && p.y > transform.position.y - 0.1 && p.y < transform.position.y + 0.1)
                {
                    pointChecker += 1;

                }

                if (p.x > transform.position.x + width - 0.1 && p.x < transform.position.x + width + 0.1
                    && p.y > transform.position.y - 0.1 && p.y < transform.position.y + 0.1)
                {
                    pointChecker += 1;

                }

                if (p.x > transform.position.x - 0.1 && p.x < transform.position.x + 0.1
                    && p.y > transform.position.y - width - 0.1 && p.y < transform.position.y - width + 0.1)
                {
                    pointChecker += 1;

                }

                if (p.x > transform.position.x - 0.1 && p.x < transform.position.x + 0.1
                    && p.y > transform.position.y + width - 0.1 && p.y < transform.position.y  + width + 0.1)
                {
                    pointChecker += 1;

                }

                if (p.x > transform.position.x - width - 0.1 && p.x < transform.position.x - width + 0.1
                    && p.y > transform.position.y - width - 0.1 && p.y < transform.position.y - width + 0.1)
                {
                    pointChecker += 1;

                }

                if (p.x > transform.position.x  + width - 0.1 && p.x < transform.position.x + width + 0.1
                    && p.y > transform.position.y + width - 0.1 && p.y < transform.position.y + width + 0.1)
                {
                    pointChecker += 1;

                }

                if (pointChecker >= 4)
                {
                    hit = true;
                    currentSlider.value += 1 / 500f;
                    if (currentSlider.value == 1)
                    {
                        animalName = this.name;
                        captureDetector.isElephantCaptured = true;
                        scoreManager.elephantCount -= 1;
                        Destroy(currentSlider.gameObject);
                        Destroy(this.gameObject);
                        break;
                    }
                }

            }
            if (hit == false)
            {
                if (currentSlider.value > 0)
                {
                    currentSlider.value -= 1 / 1500f;
                }
            }

        } 
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
        Vector2 direction = moveSpots.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 3f * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, moveSpots.position) < 0.1f){
            moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waitTime = startWaitTime;
        }

	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yay");
    }


}
