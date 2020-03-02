using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollisionHandler : MonoBehaviour {

    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smoothing = 10.0f;
    Vector3 dollyDirection;
    public Vector3 dollyDirectionAdjusted;
    public float distance;
    
	void Awake ()
    {
        // Sets these variables upon awake
        dollyDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
	}
	
	void Update ()
    {
        // This sets the desired position of the camera when not being clipped.
        Vector3 desiredCamPosition = transform.parent.TransformPoint(dollyDirection * maxDistance);
        RaycastHit hit;

        // This creates a linecast between the camera position and it's desired position and if
        // there is an obstacle, handles it accordingly by clamping the distance the camera should be
        // from the player based on the distance of the linecast.
        if (Physics.Linecast(transform.parent.position, desiredCamPosition, out hit))
        {
            distance = Mathf.Clamp ((hit.distance * 0.7f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        // This actually moves the camera position with respect to the linecast above while adding smoothing.
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, Time.deltaTime * smoothing);
	}
}
