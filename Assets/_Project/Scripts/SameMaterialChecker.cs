using UnityEngine;  
using UnityEngine.Events;  

public class SameMaterialChecker : MonoBehaviour  
{  
    // Public array to store GameObjects  
    public GameObject[] gameObjects;  

    // Unity event to invoke when materials are the same  
    public UnityEvent WhenSame;  

    // Public string to specify the material name to check  
    public string MaterialName;  

    void Update()  
    {  
        // Check if all materials are the same and match the specified MaterialName  
        if (AreMaterialsSame() && AreMaterialsNamed(MaterialName))  
        {  
            // Invoke the event if materials are the same and match the name  
            WhenSame.Invoke();  
        }  
    }  

    // Method to check if all GameObjects have the same material  
    private bool AreMaterialsSame()  
    {  
        if (gameObjects.Length == 0)  
            return false;  

        // Get the material of the first GameObject  
        Material firstMaterial = GetMaterial(gameObjects[0]);  

        // Compare with the material of the other GameObjects  
        for (int i = 1; i < gameObjects.Length; i++)  
        {  
            Material currentMaterial = GetMaterial(gameObjects[i]);  
            if (currentMaterial == null || currentMaterial != firstMaterial)  
            {  
                return false; // Materials are not the same  
            }  
        }  

        return true; // All materials are the same  
    }  

    // Method to check if all GameObjects have the specified material name  
    private bool AreMaterialsNamed(string materialName)  
    {  
        if (string.IsNullOrEmpty(materialName))  
            return false;  

        // Get the material of the first GameObject  
        Material firstMaterial = GetMaterial(gameObjects[0]);  

        // Check if the first material's name matches the specified name  
        if (firstMaterial == null || firstMaterial.name != materialName)  
            return false;  

        // Compare with the material of the other GameObjects  
        for (int i = 1; i < gameObjects.Length; i++)  
        {  
            Material currentMaterial = GetMaterial(gameObjects[i]);  
            if (currentMaterial == null || currentMaterial.name != materialName)  
            {  
                return false; // Material name does not match  
            }  
        }  

        return true; // All materials have the specified name  
    }  

    // Helper method to get the material of a GameObject  
    private Material GetMaterial(GameObject obj)  
    {  
        Renderer renderer = obj.GetComponent<Renderer>();  
        return renderer != null ? renderer.sharedMaterial : null; // Use sharedMaterial to avoid instance issues  
    }  
}