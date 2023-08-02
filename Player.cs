using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Reference to the character controller
    private CharacterController characterController;
    //Movement speed
    public float speed;
    //Turn speed
    public float lookSpeed;
    //Input manager reference
    private StarterAssetsInputs inputs;
    //Player camera reference
    public Camera playerCamera;
    //Threshold in degrees for how far up or down the player can look
    public float lookCutoff = 80f;

    private void Start()
    {
        //Get character controller
        characterController = GetComponent<CharacterController>();

        //Enable inputs
        inputs = new StarterAssetsInputs();
        inputs.Enable();

        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get movement input
        Vector2 moveInput = inputs.Player.Move.ReadValue<Vector2>();
        //Convert to a Vector3 and move
        Vector3 moveDelta = new Vector3(moveInput.x, 0, moveInput.y);
        characterController.SimpleMove(transform.rotation * moveDelta * speed);

        //Get look input
        Vector2 lookInput = inputs.Player.Look.ReadValue<Vector2>();
        //Rotate Y axis
        transform.Rotate(new Vector3(0, lookInput.x * lookSpeed, 0));

        //Rotate x axis
        playerCamera.transform.Rotate(new Vector3(lookInput.y * lookSpeed, 0, 0));

        //Adjust x axis rotation if it is beyond threshold
        float newCameraAngle = playerCamera.transform.rotation.eulerAngles.x;
        if (newCameraAngle > 180)
        {
            newCameraAngle -= 360;
        }

        if(newCameraAngle > lookCutoff)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(lookCutoff, 0, 0);
        } else if (newCameraAngle < -lookCutoff)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(-lookCutoff, 0, 0);
        }
    }
}
