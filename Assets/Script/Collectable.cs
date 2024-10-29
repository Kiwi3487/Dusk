using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectables : MonoBehaviour
{
    public DarknessAndVision darknessAndVision; // Reference to the Darkness and Vision script
    public GameObject[] collectibles;
    public GameObject exit;

    private int collectedCount = 0; //counter

    void Update()
    {
        // Check if darkness is maxed out in the other script then calls a method to switching scene
        if (darknessAndVision.IsMaxDarkness())
        {
            SwitchScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            // Disable the collectible object
            other.gameObject.SetActive(false);
            
            // Adjust darkness and FOV
            darknessAndVision.DecreaseDarknessIntensity(0.05f); // Decrease darkness
            darknessAndVision.IncreaseFieldOfView(0.5f); // Increase FOV

            // Update collected count and check if all items are collected
            collectedCount++;
            if (collectedCount == collectibles.Length)
            {
                EnableExit();
            }
        }
    }

    private void EnableExit()
    {
        if (exit != null)
        {
            exit.SetActive(true);
        }
    }

    private void SwitchScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Lose");
    }
}