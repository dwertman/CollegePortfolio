using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovementScript : MonoBehaviour {

    public Transform moveTo;
    InteractionState interactState;
    
    void Awake()
    {
        interactState = this.GetComponent<InteractionState>();
    }

    // Update is called once per frame
    void Update () {
		if (interactState.getIsActive())
        {
            MoveOrb();
        }
	}

    public void MoveOrb()
    {
        this.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(moveTo.position.x, moveTo.position.y, moveTo.position.z), 0.5f * Time.deltaTime);
    }
}
