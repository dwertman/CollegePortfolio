/*
 *Hotel California ItemData
 * This is a scriptable object for Item Data, consisting of an item name and a sequence
 *Created by Dillon Wertman
 *5/3/2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows creation of item data object from asset menu
[System.Serializable]
[CreateAssetMenu(menuName = "GameObject/Item")]

public class ItemData : ScriptableObject {

    public string itemName; //name of item
    public int itemSequence; // used to see if player has picked up items in correct order

    //get name of item
    public string Name
    {
        get
        {
            return itemName;
        }
    }

    //get sequence of item
    public int Sequence
    {
        get
        {
            return itemSequence;
        }
    }



}
