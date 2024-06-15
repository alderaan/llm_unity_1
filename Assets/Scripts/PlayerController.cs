using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CameraController camContr;
    public float moveSpeed = 10.0f; // The speed at which the ship moves horizontally/vertically
    private float constantUpwardSpeed;
    private float horizontalInput;
    private float verticalInput;
    private CameraController.CameraBounds bounds;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    public float touchSpeedVert;
    public float touchSpeedHoriz;
    public float rotationFactor = 1.0f;  // Adjust this in the Unity editor
    private const float maxRotation = 45.0f;  // The maximum rotation of the spaceship
    public bl_Joystick Joystick;
    public float inputSmoothTime = 1f;
    private float horizontalVelocity = 0.0f;
    private float verticalVelocity = 0.0f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Calculate the bottom center position of the screen
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = spriteRenderer.bounds.extents.y;
        Vector3 startPosition = new Vector3(
            bounds.Center.x,
            bounds.BottomLeft.y + halfPlayerHeight * 5f,
            0
        );

        // Set the player's initial position to the bottom center of the screen
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (! gameManager.isALive)
        {
            return;
        }

        //verticalInput = Joystick.Vertical * touchSpeedVert;
        //horizontalInput = Joystick.Horizontal * touchSpeedHoriz;

        float targetHorizontalInput = Joystick.Horizontal * touchSpeedHoriz;
        float targetVerticalInput = Joystick.Vertical * touchSpeedVert;

        horizontalInput = Mathf.SmoothDamp(horizontalInput, targetHorizontalInput, ref horizontalVelocity, inputSmoothTime);
        verticalInput = Mathf.SmoothDamp(verticalInput, targetVerticalInput, ref verticalVelocity, inputSmoothTime);


         // Calculate rotation based on horizontal input
        float targetRotation = maxRotation * horizontalInput * rotationFactor;
        targetRotation = Mathf.Clamp(targetRotation, -maxRotation, maxRotation);
        transform.rotation = Quaternion.Euler(0, 0, -targetRotation);  // Negative to match direction

        // The rest of your Update method...
        constantUpwardSpeed = camContr.currentSpeed;

        // Calculate the new position of the ship
        Vector3 newPosition = transform.position + new Vector3(
            horizontalInput * moveSpeed * Time.deltaTime, 
            constantUpwardSpeed * Time.deltaTime + verticalInput * moveSpeed * Time.deltaTime,
            0
        );

        // Make sure the new position isn't outside the camera's bounds
        bounds = CameraController.GetCameraBounds(camContr.GetComponent<Camera>());

        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = spriteRenderer.bounds.extents.y;

        newPosition.x = Mathf.Clamp(newPosition.x, bounds.BottomLeft.x + halfPlayerWidth, bounds.TopRight.x - halfPlayerWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.BottomLeft.y + halfPlayerHeight * 5f, bounds.TopRight.y - halfPlayerHeight);

        // Move the ship to the new position
        transform.position = newPosition;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.PlayerDeath();
    }
}



