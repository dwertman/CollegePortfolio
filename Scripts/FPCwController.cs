/**
 * Basic first person controller movement script for player, accepts controller input
 * created by Dillon Wertman
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.InputSystem;

public class FPCwController : MonoBehaviour
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

    Vector2 move;
    Vector2 rotate;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.CameraRotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.CameraRotate.canceled += ctx => rotate = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraT = GetComponentInChildren<Camera>().transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
            
            //Debug.Log("FPC: Player Has Authority: ");
            Cursor.lockState = CursorLockMode.Locked;

        transform.Rotate(Vector3.up * rotate.x * Time.deltaTime * mouseSensitivityX);
        //Debug.Log(transform);
        verticalLookRotation += rotate.y * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(move.x, 0, move.y).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);


    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }


}
