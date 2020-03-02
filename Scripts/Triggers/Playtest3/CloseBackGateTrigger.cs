using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBackGateTrigger : MonoBehaviour {

    public BackDoorPuzzle backDoorGate;
    public Light playerSpotLight;

    private void OnTriggerEnter(Collider other)
    {
        backDoorGate.ResetLevers();
        playerSpotLight.enabled = false;
    }
}
