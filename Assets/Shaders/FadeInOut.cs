using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    //Variables
    public GameObject player;
    public float fadeDistance = 5f;
    public float maxFadeDistance = 10f;

    public Color startColor = new Color(1f, 1f, 1f, 0f);
    public Color endColor = new Color(1f, 1f, 1f, 1f);

    private Renderer objectRenderer;
    private Material objectMaterial; 

    private void Start()
    {
        //Calls and identifies some of these variables
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = new Material(objectRenderer.material);
        objectRenderer.material = objectMaterial;
        
        objectMaterial.color = startColor;
    }

    private void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(player.transform.position, transform.position); //find distance between the object and the player
        
        float t = Mathf.Clamp01((distance - fadeDistance) / (maxFadeDistance - fadeDistance)); //get an value from 0-1 based on how close th player is by subtracting distance between the 2 object and dividing by the distance it should be at the ending color by when it starts fading


        objectMaterial.color = Color.Lerp(endColor, startColor, t); //changes the color using the function color lerp
    }
}