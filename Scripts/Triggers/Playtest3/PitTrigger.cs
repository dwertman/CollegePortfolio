using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitTrigger : MonoBehaviour {

    public Transform respawn;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = respawn.position;
    }
}
