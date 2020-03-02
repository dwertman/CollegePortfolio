using System.Collections;
using UnityEngine;
using TMPro;

public class BackDoorPuzzle : MonoBehaviour
{

    public GameObject[] levers;
    public bool doorIsOpen = false;
    public TextMeshProUGUI notificationText;

    private bool notifyOpen = false;
    private InteractionState lever0;


    private void Awake()
    {
        lever0 = levers[0].GetComponent<InteractionState>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lever0.getIsActive())
        {
            doorIsOpen = true;
        }

        // If solution is invalidated, door closes again.
        if (!lever0.getIsActive() && doorIsOpen)
        {
            doorIsOpen = false;
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
        float time = 25f;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return 0;
        }

        if (time <= 0f)
            notifyOpen = false;
    }

    public void ResetLevers()
    {
        lever0.setIsActive(false);
    }
}