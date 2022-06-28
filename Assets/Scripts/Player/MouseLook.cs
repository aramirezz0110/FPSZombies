using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensibility = 750f;
    [SerializeField] Transform playerBody;
    float mouseX;
    float mouseY;

    //to limit the rotation
    private float xRotation;
    void Start()
    {
        //to look cursor mouse in game mode
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X")*mouseSensibility*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y")*mouseSensibility*Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90); //to stablish the limit

        playerBody.Rotate(Vector3.up*mouseX);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //playerBody.Rotate(Vector3.right*mouseY);
    }
}
