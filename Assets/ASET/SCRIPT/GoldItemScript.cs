using UnityEngine;

public class GoldItemScript : MonoBehaviour
{
    // Public single GameObject that contains the materials
    public GameObject Material;
    
    // The material color used to change material to Gold
    public Material Gold;

    // Game Object to trigger to store the original materials to revert back
    public GameObject Barrier;

    // Store the original materials to revert back
    private Material[][] originalMaterials; // Array to store original materials for each MeshRenderer

    private bool isGoldApplied = false;

    private void Start()
    {
        if (Material != null)
        {
            // Get all MeshRenderer components in the GameObject and its children
            MeshRenderer[] meshRenderers = Material.GetComponentsInChildren<MeshRenderer>();

            // Initialize the original materials array to store materials for each mesh
            originalMaterials = new Material[meshRenderers.Length][];

            // Store the original materials for each MeshRenderer
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // Store all original materials used by the renderer (for all submeshes)
                originalMaterials[i] = meshRenderers[i].sharedMaterials;
            }
        }
    }

    private void Update()
    {
        // Check if the Barrier is active in the hierarchy
        if (Barrier != null && Barrier.activeInHierarchy && !isGoldApplied)
        {
            ApplyGoldMaterial();
        }
        // Check if the Barrier is not active and the gold material has been applied
        else if ((Barrier == null || !Barrier.activeInHierarchy) && isGoldApplied)
        {
            RevertToOriginalMaterial();
        }
    }

    private void ApplyGoldMaterial()
    {
        if (Material != null)
        {
            MeshRenderer[] meshRenderers = Material.GetComponentsInChildren<MeshRenderer>();

            // Change all the materials to Gold
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                Material[] goldMaterials = new Material[meshRenderers[i].sharedMaterials.Length];

                // Replace all materials with the gold material
                for (int j = 0; j < goldMaterials.Length; j++)
                {
                    goldMaterials[j] = Gold;
                }

                // Apply the gold material to all submeshes/material slots
                meshRenderers[i].sharedMaterials = goldMaterials;
            }

            isGoldApplied = true;
        }
    }

    private void RevertToOriginalMaterial()
    {
        if (Material != null)
        {
            MeshRenderer[] meshRenderers = Material.GetComponentsInChildren<MeshRenderer>();

            // Revert to the original materials
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // Re-apply the original materials to all submeshes/material slots
                meshRenderers[i].sharedMaterials = originalMaterials[i];
            }

            isGoldApplied = false;
        }
    }
}
