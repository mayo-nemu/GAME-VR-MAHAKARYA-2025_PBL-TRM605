using UnityEngine;  

public class MaterialChangerByEvent : MonoBehaviour  
{  
    // Public array to store materials  
    public Material[] materials;  

    // Index to track the current material  
    private int currentMaterialIndex = 0;  

    // Renderer component to apply materials  
    private Renderer objectRenderer;  

    void Start()  
    {  
        // Get the Renderer component attached to the GameObject  
        objectRenderer = GetComponent<Renderer>();  

        // Set the initial material to the first one in the array  
        if (materials.Length > 0)  
        {  
            objectRenderer.material = materials[0];  
        }  
    }  

    // Public function to change the material  
    public void ChangeColor()  
    {  
        // Increment the index and wrap around if necessary  
        currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;  

        // Change the material to the new one  
        objectRenderer.material = materials[currentMaterialIndex];  
    }  
}