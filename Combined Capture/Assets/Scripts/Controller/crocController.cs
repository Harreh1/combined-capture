using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crocController : MonoBehaviour {

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

    public Transform firework;

    public Slider captureProgress;
    public GameObject canvas;
    public Slider currentSlider;
    List<Vector3> collisionPoints;

    public Sprite crocHappy;
    public Sprite crocSad;
    public SpriteRenderer sp;
    public static string animalName;

    public bool hit;
    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(true);
        waitTime = startWaitTime;
        avoidingMultipler = 0;
        moveSpots = moveSpotsArray[Random.Range(0, 8)];
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        canvas = GameObject.Find("Canvas");
        sp = GetComponent<Renderer>() as SpriteRenderer;
        currentSlider = Instantiate(captureProgress);
        currentSlider.transform.position = transform.position;
        currentSlider.transform.SetParent(canvas.transform);
        currentSlider.transform.localScale -= new Vector3(45,45,0);
        hit = false;
    }
	
	// Update is called once per frame
	void Update () {
        hit = false;
        Vector3 currentpos = transform.position;
        if (currentSlider.value == 0)
        {
            sp.sprite = crocSad;
            currentpos.y += 20f;
        } else
        {
            currentpos.y += 1f;
        }
        currentSlider.transform.position = currentpos;

        collisionPoints = DepthViewTest.circlePositions;
        if (DepthViewTest.circlePositions != null)
        {
            foreach(var p in collisionPoints)
            {
                if (p.x > transform.position.x - 0.1 && p.x < transform.position.x + 0.1
                    && p.y > transform.position.y - 0.1 && p.y < transform.position.y + 0.1)
                {
                    sp.sprite = crocHappy;
                    hit = true;
                    currentSlider.value += 1 / 2.5f;
                    if (currentSlider.value == 1)
                    {
                        animalName = this.name;
                        captureDetector.isCrocCaptured = true;
                        scoreManager.crocCount -= 1;
                        Instantiate(firework, new Vector3(0, 0, 1), Quaternion.identity);
                        Destroy(currentSlider.gameObject);
                        Destroy(this.gameObject);
                    }
                }
            }
            if (hit == false)
            {
                if (currentSlider.value > 0)
                {
                    currentSlider.value -= 1 / 60f;
                }
            }

        } 
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
        Vector2 direction = moveSpots.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
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
