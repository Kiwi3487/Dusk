using UnityEngine;

public class ToggleMaterialPerObject : MonoBehaviour
{
    [System.Serializable]
    public class ObjectsMaterials
    {
        public GameObject objects;
        public Material alternateMaterial; 
    }

    public ObjectsMaterials[] objectsMaterials; 

    private Material[] originalMaterials;
    private bool isUsingDefaultMaterials; 

    void Start()
    {
        // stores current texture
        originalMaterials = new Material[objectsMaterials.Length];
        for (int i = 0; i < objectsMaterials.Length; i++)
        {
            if (objectsMaterials[i].objects != null)
            {
                Renderer renderer = objectsMaterials[i].objects.GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterials[i] = renderer.material;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleMaterials();
        }
    }

    void ToggleMaterials()
    {
        //change all materials 
        for (int i = 0; i < objectsMaterials.Length; i++)
        {
            var pair = objectsMaterials[i];
            if (pair.objects != null)
            {
                Renderer renderer = pair.objects.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (isUsingDefaultMaterials)
                    {
                        // change to better texture if true
                        renderer.material = originalMaterials[i];
                    }
                    else
                    {
                        // change to better texture if false
                        renderer.material = pair.alternateMaterial;
                    }
                }
            }
        }
        isUsingDefaultMaterials = !isUsingDefaultMaterials; //truefalse change
    }
}
