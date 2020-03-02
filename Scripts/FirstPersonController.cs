/**
 * Basic first person controller movement script for player, accepts keyboard input
 * created by Dillon Wertman
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    PlayerControls controls;
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float walkSpeed = 8;

    Transform cameraT;
    float verticalLookRotation;
    new Rigidbody rigidbody;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    private void Awake()
    {
        controls = new PlayerControls(); 

        //controls.Gameplay.CameraRotate.performed += 
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraT = GetComponentInChildren<Camera>().transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (isLocalPlayer)
        //{
            
            //Debug.Log("FPC: Player Has Authority: ");
            Cursor.lockState = CursorLockMode.Locked;
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
            verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
            cameraT.localEulerAngles = Vector3.left * verticalLookRotation;


            Vector3 moveDir = new Vector3(Input.GetAxisRaw("MHorizontal"), 0, Input.GetAxisRaw("MVertical")).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        //}


    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }
}
