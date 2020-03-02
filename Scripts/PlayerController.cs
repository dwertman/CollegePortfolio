/*
 *Hotel California PlayerController
 * This is the player controller for Hotel California, it handles player movement and door interaction
 *Created by Dillon Wertman
 *5/3/2019
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed;                     //player speed
    private bool isFlipped;                 //used to flip sprite
    Rigidbody2D rb;                         //rigidbody of game object
    private bool playerInOpenDoor = false;  //used to see if player is in a door to go to another scene
    private bool playerInWinningDoor = false; //used to see if player is in door to end of level




    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>(); //get rigidbody of player
        isFlipped = false;
    }

    //called every frame
    void Update()
    {
        //end game if player is in winning door
        if (Input.GetKey(KeyCode.W) && playerInOpenDoor)
        {
            if (playerInWinningDoor)
            {
                GameManager.endGame(true);
            }
            else //switch scenes if player is in open door
            {
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        
    }

    
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //flip character if moving left
        if(moveHorizontal == -1 && !isFlipped)
        {
            isFlipped = true;
            flip();
        }
        //flip character if moving right
        else if (moveHorizontal == 1 && isFlipped)
        {
            isFlipped = false;
            flip();
        }
        //move player object
        rb.AddForce(movement * speed);

	}

    //flips player sprite
    void flip()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.flipX = !sr.flipX;
    }

    /*
     * Checks if player can move through doors and if the door is the end of the level
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OpenDoor")
        {
            playerInOpenDoor = true;
        }
        if (collision.gameObject.tag == "WinDoor")
        {
            playerInWinningDoor = true;
        }
        


    }

    /*
     * Makes sure player can't open a door unless they are in the doorframe
     */ 
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInOpenDoor = false;
        playerInWinningDoor = false;
    }





}
