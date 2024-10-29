using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLutShader : MonoBehaviour
{
    public Shader lutShader = null; // Assign your LUT shader here
    public Material m_renderMaterial; // Material to hold the shader
    private bool isLutEnabled = true; // Track whether the LUT effect is enabled
    private const float contribution = 0.208f; // Fixed contribution value

    void Start()
    {
        if (lutShader != null)
        {
            m_renderMaterial = new Material(lutShader);
        }
    }

    void Update()
    {
        // Toggle the LUT effect when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            isLutEnabled = !isLutEnabled;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Only apply the LUT effect if it is enabled
        if (isLutEnabled && m_renderMaterial != null)
        {
            m_renderMaterial.SetFloat("_Contribution", contribution); // Set the fixed contribution
            Graphics.Blit(source, destination, m_renderMaterial);
        }
        else
        {
            Graphics.Blit(source, destination); // Copy the source to destination if effect is not applied
        }
    }
}