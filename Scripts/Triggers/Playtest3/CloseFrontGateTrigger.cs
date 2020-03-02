using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFrontGateTrigger : MonoBehaviour {

    public FrontDoorPuzzle frontDoorGate;
    public Light playerSpotLight;

    private void Awake()
    {
        playerSpotLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        frontDoorGate.ResetLevers();
        playerSpotLight.enabled = true;
    }
}
