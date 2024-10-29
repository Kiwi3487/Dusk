using UnityEngine;

public class RenderShader : MonoBehaviour
{
    public Material activatedMaterial; // Material to switch to when player is close
    public float activationDistance = 5f; // Distance at which shader activates
    private Material originalMaterial; // Store the original material to revert back to
    private Renderer objectRenderer; // Renderer of the object

    private Transform playerTransform;

    private void Start()
    {
        // Cache references
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material; // Store the original material
        playerTransform = GameObject.FindWithTag("Player").transform; // Find the player by tag
    }

    private void Update()
    {
        // Check distance between player and object
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        
        // Activate or revert material based on distance
        if (distance <= activationDistance)
        {
            objectRenderer.material = activatedMaterial;
        }
        else
        {
            objectRenderer.material = originalMaterial; 
        }
    }
}
