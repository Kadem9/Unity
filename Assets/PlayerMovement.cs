using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float rotationSpeed = 100f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private bool isPaused = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isPaused)
        {
            // Déplacement
            float moveDirection = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            Vector3 move = transform.forward * moveDirection;
            controller.Move(move);

            // Rotation avec les flèches directionnelles
            float rotationDirection = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationDirection, 0);

            // Rotation avec la souris
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
    }
}
