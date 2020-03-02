//This script can be included on any NPC in order to allow them to be interacted with.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public InteractionState interactState;
    public bool isActive;
    public Dialogue dialogue;

    public bool inDialogue;

    private void Start()
    {
        //StartDialogue();
    }

    private void Update()
    {
        isActive = interactState.getIsActive();
        if (isActive == true && inDialogue == false)
        {
            inDialogue = true;
            StartDialogue();
        }
        if (inDialogue == true)    //sets the object this script is attached to, to destroy itself
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().BeginDialogue(dialogue);
    }

}
