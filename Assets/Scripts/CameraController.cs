using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float initialSpeed = 1.0f;
    public float speedIncreasePerSecond = 0.1f;
    public float currentSpeed;
    public Camera mainCam;
    public struct CameraBounds
    {
        public Vector3 BottomLeft;
        public Vector3 TopRight;
        public Vector3 TopLeft;
        public Vector3 BottomRight;
        public Vector3 Center;
    }
    public GameManager gameManager;
    public GameObject player;

    public static CameraBounds GetCameraBounds(Camera mainCamera)
    {
        CameraBounds bounds;

        bounds.BottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        bounds.TopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        bounds.TopLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        bounds.BottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        bounds.Center = (bounds.BottomLeft + bounds.TopRight) / 2;

        return bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isALive)
        {
            currentSpeed = 0;
            return;
        }

        currentSpeed = initialSpeed + speedIncreasePerSecond * gameManager.timeSinceGameStart;
        // transform.position += new Vector3(0, currentSpeed * Time.deltaTime, 0);
        Vector3 ptp = player.transform.position;
        transform.position = new Vector3(ptp.x,ptp.y,-10); //
    }

}
