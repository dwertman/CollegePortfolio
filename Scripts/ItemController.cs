/*
 *Hotel California ItemController
 * This is the item controller for Hotel California, it handles items being picked up and being added to player inventory
 *Created by Dillon Wertman
 *5/3/2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public ItemData itemData;
    public GameManager gm;
    public static GameObject addedItem;

    ItemController instanceOfItem = null;
    GameObject collisionObject;
    bool playerInCollision = false;

    

    //destroy item on scene if player has it in inventory
    private void Awake()
    {

        if (GameManager.playerInventory.ContainsValue(itemData.Name))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

        //allows player to pick up item and add to inventory
        if (Input.GetKey(KeyCode.Space) && playerInCollision)
        {
            GameManager.playerInventory.Add(itemData.Sequence, itemData.Name);

            
            Destroy(gameObject);
            playerInCollision = false;
        }
    }

    //checks if player is in collision wqith itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collisionObject = collision.gameObject;
            playerInCollision = true;
        }

    }

    //player is no longer colliding with item
    private void OnTriggerExit2D(Collider2D collision)
    {

        playerInCollision = false;
    }
}
