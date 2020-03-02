/*
 *Hotel California EnemyController
 * This is the enemy controller for Hotel California, it handles enemy movement and player interaction
 *Created by Dillon Wertman
 *5/3/2019
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    
    private bool flip;
    private int direction;
    private SpriteRenderer sr;
    delegate void KillPlayer();
    KillPlayer killPlayer;
    GameManager gm;
    private int distance = 10;
    private float movement = 0.5f;
    private bool lose;


    // Starts coroutine to move enemy when it is spawned
    IEnumerator Start () {
        direction = 1;
        flip = false;
        sr = gameObject.GetComponent<SpriteRenderer>();
       yield return StartCoroutine("moveEnemy");
    }

    /*
     *Coroutine to move enemy with interpolation 
     */ 
    IEnumerator moveEnemy(){


        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 1 * direction, transform.position.y), Time.deltaTime);
            distance -= 2;
            if(distance <= 0)
            {
                distance = 1000;
                movement = movement * -1;
            }
            yield return null;
        }
    }

    /*
     * Flips sprite if a wall is hit and moves enemy in opposite direction
     * Destroys player if it is hit, stops enemy, notifies game manager to end game
     */ 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //turn around if a wall is hit
        if (collision.gameObject.CompareTag("Wall"))
        {
            //change direction
            direction = direction * -1; 

            //flip sprite
            flip = !flip;
            sr.flipX = flip;
        }

        //destroy player, stop moveing, end the game
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            StopCoroutine("moveEnemy");
            lose = true;
            GameManager.endGame(true);
        }
    }
}
