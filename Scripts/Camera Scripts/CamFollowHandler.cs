using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowHandler : MonoBehaviour {

    // In input settings, for controller support with the right analog stick,
    // the 4th axis is Right Stick Horizontal and the 5th axis is Right Stick Vertical.

    public float modeChangeSmoothing = 1.0f;

    public float camMoveSpeed = 120.0f;
    public GameObject camFollowObject_NormalMode;
    public GameObject camFollowObject_InteractionMode;
    Vector3 followPosition;
    public float verticalClamp = 55.0f;
    public float inputSensitivity = 150.0f;
    GameObject camObject;
    GameObject playerObject;
    float mouseX;
    float mouseY;
    float finalInputX;
    float finalInputZ;
    Transform toFollow;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private bool isInInteractionMode = false;

    void Start ()
    {
        // Gets the local rotation of this object and assigns it to
        // the respective rotation variables.
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
	}

	void Update ()
    {
        // Reads in axis of controller right analog stick and sets it to variables.
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");

        // Reads in axis of mouse and sets it to variables.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Sets final input to the sum of the above respective directions. This will
        // set the mouse movement to whatever is being used at the time due to the
        // other input being 0 when not in use.
        // NOTE: May behave strangely if both controller and mouse are used simultaneously.
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        // Rotates the camera based on player input, modified by input sensitivity.
        rotationX += finalInputZ * inputSensitivity * Time.deltaTime;
        rotationY += finalInputX * inputSensitivity * Time.deltaTime;

        // Clamps the X rotation within parameters to prevent the camera from moving
        // outside our set angles.
        rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);
        // This actually rotates the camera by setting it's transform.rotation to a quaternion.
        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        transform.rotation = localRotation;

        // Check for interaction mode
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInInteractionMode = !isInInteractionMode;
        }
    }

    void LateUpdate ()
    {
        // Calls UpdateCamera() method
        UpdateCamera();
    }

    void UpdateCamera()
    {
        // Sets what object to follow with the camera
        if (!isInInteractionMode)
        {
            toFollow = camFollowObject_NormalMode.transform;

        }
        else if (isInInteractionMode)
        {
            toFollow = camFollowObject_InteractionMode.transform;
        }

        // Moves the camera towards the object that it is following
        float takeStep = camMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, toFollow.position, takeStep);
    }
}
