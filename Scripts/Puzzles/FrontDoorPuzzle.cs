using System.Collections;
using UnityEngine;
using TMPro;

public class FrontDoorPuzzle : MonoBehaviour
{

    public GameObject[] levers;
    public bool doorIsOpen = false;
    public TextMeshProUGUI notificationText;

    private bool notifyOpen = false;
    private InteractionState lever0, lever1, lever2, lever3, lever4, lever5;


    private void Awake()
    {
        lever0 = levers[0].GetComponent<InteractionState>();
        lever1 = levers[1].GetComponent<InteractionState>();
        lever2 = levers[2].GetComponent<InteractionState>();
        lever3 = levers[3].GetComponent<InteractionState>();
        lever4 = levers[4].GetComponent<InteractionState>();
        lever5 = levers[5].GetComponent<InteractionState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Solution: 100110

        if (lever0.getIsActive() && !lever1.getIsActive() && !lever2.getIsActive() && lever3.getIsActive() && lever4.getIsActive() && !lever5.getIsActive() && !doorIsOpen)
        {
            notifyOpen = true;
            doorIsOpen = true;
        }

        // If solution is invalidated, door closes again. (Opposite of solution above): 011001
        if ((!lever0.getIsActive() || lever1.getIsActive() || lever2.getIsActive() || !lever3.getIsActive() || !lever4.getIsActive() || lever5.getIsActive()) && doorIsOpen)
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
        lever1.setIsActive(false);
        lever2.setIsActive(false);
        lever3.setIsActive(false);
        lever4.setIsActive(false);
        lever5.setIsActive(false);
    }
}