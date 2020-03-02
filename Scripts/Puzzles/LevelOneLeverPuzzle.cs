using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelOneLeverPuzzle : MonoBehaviour
{

    public GameObject[] levers;
    public bool doorIsOpen = false;
    public TextMeshProUGUI notificationText;

    private bool notifyOpen = false;
    private InteractionState lever0, lever1, lever2;


    private void Awake()
    {
        lever0 = levers[0].GetComponent<InteractionState>();
        lever1 = levers[1].GetComponent<InteractionState>();
        lever2 = levers[2].GetComponent<InteractionState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Solution: 100110

        if (lever0.getIsActive() && lever1.getIsActive() && lever2.getIsActive() && !doorIsOpen)
        {
            notifyOpen = true;
            doorIsOpen = true;
        }

        // If solution is invalidated, door closes again. (Opposite of solution above): 011001
        if ((!lever0.getIsActive() || !lever1.getIsActive() || !lever2.getIsActive()) && doorIsOpen)
        {
            doorIsOpen = false;
        }

        if (doorIsOpen)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<MeshCollider>().enabled = false;
        }

        if (!doorIsOpen)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<MeshCollider>().enabled = true;
        }

        // Notifies player when they have successfully opened the gate
        StartCoroutine(NotifyOpenGate());

        if (notifyOpen)
        {
            notificationText.enabled = true;
        }
        else
        {
            notificationText.enabled = false;
        }
    }

    // Timer for how long notification should stay on screen
    IEnumerator NotifyOpenGate()
    {
        float time = 15f;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return 0;
        }

        if (time <= 0f)
            notifyOpen = false;
    }
}