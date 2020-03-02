//Player keeps track of player data
//created by Dillon Wertman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int playerScore;
    bool fireInput;
    private float startTime;
    public Text time;
    public Text score;
    public bool canStart;

    public void CanStart()
    {
        canStart = true;
    }

    private void Awake()
    {
        //time = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        //score = gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        startTime = 60f;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;

        //Debug.Log(gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text);
        //time.text = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        //score.text = gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text;
    }

    void Update()
    {

        startTime -= Time.deltaTime;
        time.text = "Time: " + startTime.ToString("f2");
        score.text = getScore().ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void Timer()
    {

        startTime -= .1f;
    }

    public int getScore()
    {
        return playerScore;
    }

    public void addScore(int score)
    {
        playerScore += score;
    }

    

    //public void postScores(int i)
    //{
    //    Main.Instance.Web.postScore(Main.Instance.getPasswords()[i], playerScore);
    //}
    
}
