using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPuzzle : MonoBehaviour {

    public GameObject RedOrb, BlueOrb, GreenOrb;
    public GameObject RedDestination, BlueDestination, GreenDestination;

    private bool redDone, blueDone, greenDone;

    private void Start()
    {
        redDone = false;
        blueDone = false;
        greenDone = false;
    }

    // Update is called once per frame
    void Update() {

        if ((Vector3.Distance(RedDestination.transform.position, RedOrb.transform.position) < .5) && !redDone)
        {
            Debug.Log("Red has landed.");
            redDone = true;
        }
        if ((Vector3.Distance(BlueDestination.transform.position, BlueOrb.transform.position) < .5) && !blueDone)
        {
            Debug.Log("Blue has landed.");
            blueDone = true;
        }
        if ((Vector3.Distance(GreenDestination.transform.position, GreenOrb.transform.position) < .5) && !greenDone)
        {
            Debug.Log("Green has landed.");
            greenDone = true;
        }

        if (greenDone && blueDone && redDone)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
