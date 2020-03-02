
/*
 *Hotel California ButtonSystem
 * This is the button system for Hotel California, it handles buttons of the main menu
 *Created by Dillon Wertman
 *5/3/2019
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Buttons : MonoBehaviour {

    public Button start; //start button
    public Button info; //info button
    public Button quit; //quit button
    public Button back; //back button
    public Canvas instructions; //instructions canvas
    public Canvas buttons; //buttons canvas

    private void Awake()
    {
        instructions.gameObject.SetActive(false);
        buttons.GetComponent<Canvas>().enabled = true;
    }

    // Use this for initialization
    void Start () {
        start.onClick.AddListener(startGame);
        info.onClick.AddListener(Info);
        quit.onClick.AddListener(Quit);
        back.onClick.AddListener(Back);

        
    }

    //quit game
    private void Quit()
    {
        Application.Quit();
    }

    //display info screen
    private void Info()
    {
        instructions.gameObject.SetActive(true);
        buttons.GetComponent<Canvas>().enabled = false;
    }

    //start game
    private void startGame()
    {
        SceneManager.LoadScene("Scene001");
    }

    private void Back()
    {
        instructions.gameObject.SetActive(false);
        buttons.GetComponent<Canvas>().enabled = true;
    }
    
    


    
}
