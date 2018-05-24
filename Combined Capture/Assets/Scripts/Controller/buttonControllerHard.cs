using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonControllerHard : MonoBehaviour {

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

    public Sprite ravenHappy;
    public Sprite ravenSad;
    public SpriteRenderer sp;

    public static string animalName;

    public bool hit;
    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(true);
        canvas = GameObject.Find("Canvas");
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
            currentpos.y += 20f;
        } else
        {
            currentpos.y += 1f;
        }
        currentSlider.transform.position = currentpos;

        collisionPoints = DepthViewTest.circlePositions;
        if (DepthViewTest.circlePositions != null)
        {
            foreach (var p in collisionPoints)
            {
                if (p.x > transform.position.x - 0.3 && p.x < transform.position.x + 0.3
                    && p.y > transform.position.y - 0.3 && p.y < transform.position.y + 0.3)
                {
                    
                    hit = true;
                    currentSlider.value += 1 / 40f;
                    if (currentSlider.value == 1)
                    {
                        SceneManager.LoadScene("leveltwo-score", LoadSceneMode.Single);
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

	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yay");
    }


}
