using UnityEngine;
using System.Collections;
using UnityEditor;

public class FlashDamage : MonoBehaviour
{
    // Public array of GameObjects that contain the materials
    public GameObject[] Materials;
    
    // The material color used to flash on damage
    public Material FlashDamageColor;

    // Duration for the flash effect
    public float flashDuration = 0.1f;
    
    // Store the original materials to revert back
    private Material[] originalMaterials;

    private void Start()
    {
        // Save the original materials of the GameObjects
        originalMaterials = new Material[Materials.Length];
        for (int i = 0; i < Materials.Length; i++)
        {
            if (Materials[i].GetComponent<Renderer>() != null)
            {
                originalMaterials[i] = Materials[i].GetComponent<Renderer>().material;
            }
        }
    }

    // Public function to initiate the flash effect
    public void TakeDamage()
    {
        StartCoroutine(FlashMaterials());
    }

    // Coroutine to handle the blinking effect
    private IEnumerator FlashMaterials()
    {
        // Change all materials to FlashDamageColor
        for (int i = 0; i < Materials.Length; i++)
        {
            if (Materials[i].GetComponent<Renderer>() != null)
            {
                Materials[i].GetComponent<Renderer>().material = FlashDamageColor;
            }
        }
        
        // Wait for flashDuration seconds
        yield return new WaitForSeconds(flashDuration);

        // Revert all materials back to their original material
        for (int i = 0; i < Materials.Length; i++)
        {
            if (Materials[i].GetComponent<Renderer>() != null)
            {
                Materials[i].GetComponent<Renderer>().material = originalMaterials[i];
            }
        }
    }
}

// Custom Editor to add a TestBlink button in the Unity Inspector
[CustomEditor(typeof(FlashDamage))]
public class FlashDamageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get reference to the FlashDamage script
        FlashDamage flashDamage = (FlashDamage)target;

        // Add a button to test the blink effect
        if (GUILayout.Button("Test Blink"))
        {
            // Call the TakeDamage function when the button is pressed
            flashDamage.TakeDamage();
        }
    }
}
