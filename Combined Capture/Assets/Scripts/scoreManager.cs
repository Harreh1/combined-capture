using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreManager : MonoBehaviour {
    public static int score = 0;
    public string animal1 = "elephant";
    public Transform animal1Object;
    public string animal2 = "raven";
    public Transform animal2Object;
    public Text text;
    public static int ravenCount = 4;
    public static int elephantCount = 2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score.ToString();
        if (elephantCount < 2)
        {
            score += 50;
            elephantCount += 1;
            StartCoroutine(spawnElephant(3));   
        }
        if (ravenCount < 4)
        {
            score += 5;
            ravenCount += 1;
            StartCoroutine(spawnRaven(1));
        }
        if (timeCountDown.endTime == "0")
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
        scoreTracker.score = score;
    }

    IEnumerator spawnElephant(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal1Object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }

    IEnumerator spawnRaven(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal2Object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
}
