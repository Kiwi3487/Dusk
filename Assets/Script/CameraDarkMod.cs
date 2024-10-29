using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DarknessAndVision : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Camera playerCamera;
    public float darknessSpeed = 0.05f;
    public float fovReductionSpeed = 0.5f;

    private Vignette vignette;
    private float originalFOV;
    private float maxDarknessIntensity = 0.6f; // Max darkness level for vignette
    private float minFOV = 30f; // Minimum field of view
    private float currentDarknessIntensity;

    void Start()
    {
        // Get the Vignette effect from the Post-Process Volume
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f; // Start with no darkness
        }
        originalFOV = playerCamera.fieldOfView; // Save original FOV
        currentDarknessIntensity = 0f;
    }

    void Update()
    {
        // Gradually increase darkness
        if (vignette != null && currentDarknessIntensity < maxDarknessIntensity)
        {
            currentDarknessIntensity += darknessSpeed * Time.deltaTime;
            vignette.intensity.value = currentDarknessIntensity;
        }

        // Gradually narrow the field of view
        if (playerCamera.fieldOfView > minFOV)
        {
            playerCamera.fieldOfView -= fovReductionSpeed * Time.deltaTime;
        }
    }

    // Method to decrease darkness intensity when an orb is collected
    public void DecreaseDarknessIntensity(float amount)
    {
        currentDarknessIntensity = Mathf.Max(0, currentDarknessIntensity - amount);
        vignette.intensity.value = currentDarknessIntensity;
    }

    // Method to increase FOV when an orb is collected
    public void IncreaseFieldOfView(float amount)
    {
        playerCamera.fieldOfView = Mathf.Min(originalFOV, playerCamera.fieldOfView + amount);
    }

    // Method to check if darkness has reached maximum intensity
    public bool IsMaxDarkness()
    {
        return currentDarknessIntensity >= maxDarknessIntensity;
    }
}
