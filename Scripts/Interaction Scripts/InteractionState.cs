using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : MonoBehaviour {
    
    [SerializeField] private bool isActive = false;

    public void setIsActive (bool state)
    {
        isActive = state;
    }

    public bool getIsActive ()
    {
        return isActive;
    }
}
