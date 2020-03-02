/*
 *Hotel California GameManager
 * This is the game manager for Hotel California, it handles spawning enemies, win state, and player inventory
 *Created by Dillon Wertman
 *5/3/2019
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //player inventory
    [SerializeField]
    public static Dictionary<int, string> playerInventory = new Dictionary<int, string>();
    
    //singleton instance variable
    public static GameManager instance = null;

    //the correct sequence the player needs to complete the level
    public static int[] correctSequence = new int[2];
    
    //canvas object for game over screens
    public Canvas canvas;

    //the canvas's animator component
    private static Animator anim;

    //GameObjects
    public GameObject doorToBeOpened;
    private GameObject spawnPoint;
    public GameObject enemy;
    public static GameObject player;

    private void Update()
    {
        //Restart game
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Scene000");
        }
    }

    private void Awake()
    {

        //singleton game manager
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        //when player enters scene, the correct sequence for inventory is checked
        if (playerInventory.Count == correctSequence.Length)
        {
            //call to check sequence method
            CheckSequence(correctSequence, playerInventory);
        }

        //instantiate anim object
        anim = canvas.GetComponent<Animator>();
    }

    /*
     * SpawnSpirit
     * Spawns enemy spirit at predefined spawn point
     */
    public void SpawnSpirit()
    {
         spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
         Instantiate(enemy, spawnPoint.transform);
    }


    /*
     * Compares sequence of items in player inventory with correct sequence
     */ 
    public void CheckSequence(int[] correctSequence, Dictionary<int, string> inventory)
    {

        int matches = 0; //matches between correct sequence and player inventory sequence
        int[] inventorySequence = inventory.Keys.ToArray(); //int array to hold keys of objects in player inventory
        
        //set each element of correct element to be consecutive numbers
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (i == 0)
                correctSequence[i] = correctSequence[i] + 1;            
            else
                correctSequence[i] = correctSequence[i - 1] + 1;
        }


        //increments number of matches by comparing sequences
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (correctSequence[i] == inventorySequence[i])
                {
                    matches++;
                }
        }

        //execute if sequences matched
        if(matches == correctSequence.Length)
        {
            //the player wins and is able to go through previously closed door
            doorToBeOpened = GameObject.FindGameObjectWithTag("WinDoor");
            doorToBeOpened.GetComponent<Animator>().SetTrigger("CorrectSequence");
        }
        else
        {
            //the player fails and the game manager spawns enemy
            SpawnSpirit();
        }
    }


    /*
     * Method to handle end game. Displays different canvases based on player win or lose
     */
    public static void endGame(bool didPlayerWin)
    {
        if (!didPlayerWin)
            anim.SetTrigger("GameOver");
        else
            anim.SetTrigger("GameWin");
    }


}
