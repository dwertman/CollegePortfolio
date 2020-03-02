/**
 * Fish keeps track of fish colliding with a hook and calls a method for adding a fish's score to a player's
 * created by Dillon Wertman
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    [SerializeField]
    private PlayerEvent OnFishCaught;

    public FishData fishData;

    private bool called = false;
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Hooked" && called == false)
        {
            OnFishCaught.Raise();
            called = true;
        }
        
    }
}
