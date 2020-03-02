using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckInteractions : MonoBehaviour {

    public float interactDistance = 4.0f;
    public TextMeshProUGUI interactControls;
    public GameObject reticlePanel;

    Ray interactionRay;
    Camera camera;
    bool isInteractionAvailable = false;

    private void Start()
    {
        camera = Camera.main;
        interactControls.enabled = false;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        interactionRay = camera.ScreenPointToRay(Input.mousePosition);

        // Checks if there is an object in front of the player and sets a boolean accordingly
        if (Physics.Raycast(interactionRay, out hit, interactDistance) && hit.transform.tag == "Interactable")
        {
            reticlePanel.SetActive(true);
            isInteractionAvailable = true;
            interactControls.enabled = true;
            //Debug.Log("INTERACTING");
        }
        else // if (!Physics.Raycast(interactionRay, out hit, interactDistance))
        {
            reticlePanel.SetActive(true);
            isInteractionAvailable = false;
            interactControls.enabled = false;
            //Debug.Log("NOT INTERACTING");
        }

        // Checks whether above boolean is true and the Interact button (Left Click) is pressed down.
        // If both are true, it sets the state of the interactable object as
        // the opposite of it's previous state. (False if true, true if false)
        if (Input.GetButtonDown("Interact") && isInteractionAvailable)
        {
            InteractionState state = hit.collider.GetComponent<InteractionState>();
            if (state != null && !state.getIsActive())
            {
                state.setIsActive(true);
                Debug.Log("Interacted With! Set to: " + state.getIsActive());
            }
            else if (state != null && state.getIsActive())
            {
                state.setIsActive(false);
                Debug.Log("Interacted With! Set to: "  + state.getIsActive());
            }
        }
    }
}
