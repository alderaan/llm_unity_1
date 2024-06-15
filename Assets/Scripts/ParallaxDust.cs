using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDust : MonoBehaviour
{
    public float speed = 0.5f;
    public ParticleSystem dustParticles1;
    public ParticleSystem dustParticles2;
    public CameraController cameraController;
    public float verticalBuffer;
    public GameManager gameManager;

    void Start()
    {
        var bounds = CameraController.GetCameraBounds(cameraController.mainCam);
        dustParticles1.transform.position = new Vector3(0,bounds.BottomLeft.y + verticalBuffer,0);
        dustParticles2.transform.position = new Vector3(0,bounds.TopLeft.y + verticalBuffer,0);

    }
    void Update()
    {
        if (!gameManager.isALive)
        {
            return;
        }

        // Move both systems down
        dustParticles1.transform.position += Vector3.down * speed * Time.deltaTime;
        dustParticles2.transform.position += Vector3.down * speed * Time.deltaTime;

        // Get the current camera bounds
        var bounds = CameraController.GetCameraBounds(cameraController.mainCam);

        // particle 1 is active and became invisible
        if (dustParticles1.transform.position.y <= bounds.BottomLeft.y - verticalBuffer)
        {
            ResetParticleSystem(dustParticles1, bounds.TopLeft.y + verticalBuffer);
        }
        if (dustParticles2.transform.position.y <= bounds.BottomLeft.y - verticalBuffer)
        {
            ResetParticleSystem(dustParticles2, bounds.TopLeft.y + verticalBuffer);
        }

    }

    void ResetParticleSystem(ParticleSystem ps, float resetHeight)
    {
        // Stop the particle system and clear the current particles
        ps.Stop();
        ps.Clear();

        // Reset position to top of camera view
        ps.transform.position = new Vector3(ps.transform.position.x, resetHeight, ps.transform.position.z);

        // Restart the particle system
        ps.Play();
    }
}


