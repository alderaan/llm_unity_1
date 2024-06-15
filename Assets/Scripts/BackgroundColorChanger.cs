using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Color[] colorPalette;  // Your color palette
    public float transitionTime = 5.0f; // Time taken to transition between colors
    private Camera cam;  // Camera to change background color
    private int currentColorIndex = 0;  // Current color
    private float transitionRate;
    private float currentTransitionTime;

    void Start()
    {
        if (colorPalette.Length < 2) {
            Debug.LogError("Need at least two colors in colorPalette for a transition to occur.");
            return;
        }

        cam = GetComponent<Camera>();  // Assuming this script is attached to your Camera
        cam.backgroundColor = colorPalette[currentColorIndex];
        transitionRate = 1.0f / transitionTime;
    }

    void Update()
    {
        if (colorPalette.Length < 2) return;

        currentTransitionTime += Time.deltaTime * transitionRate;
        
        cam.backgroundColor = Color.Lerp(colorPalette[currentColorIndex], 
            colorPalette[(currentColorIndex + 1) % colorPalette.Length], currentTransitionTime);

        if (currentTransitionTime >= 1.0f)  // Time to switch to the next color
        {
            currentColorIndex = (currentColorIndex + 1) % colorPalette.Length;
            currentTransitionTime = 0.0f;
        }
    }
}
