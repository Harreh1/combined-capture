using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreManagerHard : MonoBehaviour {
    public static int score = 0;
    public string animal1 = "elephant";
    public Transform animal1Object;
    public string animal2 = "raven";
    public Transform animal2Object;
    public Transform animal3Object;
    public Transform animal4Object;
    public Transform animal5Object;
    public Transform animal6object;
    public Transform bombObject;
    public Text text;
    public static int ravenCount = 1;
    public static int elephantCount = 1;
    public static int mouseCount = 1;
    public static int cowCount = 1;
    public static int turtleCount = 1;
    public static int crocCount = 1;
    public static int bombCount = 1;
    // Use this for initialization
    void Start () {
        score = 0;
        ravenCount = 1;
        elephantCount = 1;
        mouseCount = 1;
        cowCount = 1;
        turtleCount = 1;
        crocCount = 1;
        bombCount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score.ToString();
        Debug.Log("G");
        if (elephantCount < 1)
        {
            score += 50;
            elephantCount += 1;
            StartCoroutine(spawnElephant(4));   
        }
        if (ravenCount < 1)
        {
            Debug.Log("A");
            score += 15;
            ravenCount += 1;
            StartCoroutine(spawnRaven(3));
        }
        if (mouseCount < 1)
        {
            score += 5;
            mouseCount += 1;
            StartCoroutine(spawnMouse(2));
        }
        if (cowCount < 1)
        {
            score += 50;
            cowCount += 1;
            StartCoroutine(spawnCow(4));
        }
        if (turtleCount < 1)
        {
            score += 5;
            turtleCount += 1;
            StartCoroutine(spawnTurtle(2));
        }
        if (crocCount < 1)
        {
            score += 15;
            crocCount += 1;
            StartCoroutine(spawnCroc(2));
        }
        if (bombCount < 1)
        {
            score -= 20;
            bombCount += 1;
            StartCoroutine(spawnBomb(2));
        }
        if (timeCountDown.endTime == "0")
        {
            SceneManager.LoadScene("countdownFinished1", LoadSceneMode.Single);
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

    IEnumerator spawnMouse(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal3Object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }

    IEnumerator spawnCow(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal4Object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }

    IEnumerator spawnTurtle(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal5Object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }

    IEnumerator spawnBomb(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(bombObject, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }

    IEnumerator spawnCroc(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(animal6object, new Vector3(Random.Range(-12.5f, 12.5f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
    }
}
