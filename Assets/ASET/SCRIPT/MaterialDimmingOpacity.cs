using UnityEngine;

public class MaterialDimmingOpacity : MonoBehaviour
{
    public Renderer objectRenderer; // The renderer of the game object
    public float loopDuration = 2f; // Time for a full loop (dim and brighten)
    public float minOpacity = 0.2f; // Minimum opacity (fully dimmed)
    public float maxOpacity = 1f;   // Maximum opacity (fully bright)
    
    private Material objectMaterial;
    private Color baseColor;
    private float timeElapsed;

    void Start()
    {
        // Get the material of the object
        objectMaterial = objectRenderer.material;
        baseColor = objectMaterial.color; // Store the base color (with current alpha)
    }

    void Update()
    {
        // Calculate how far along in the loop we are
        timeElapsed += Time.deltaTime;
        float t = Mathf.PingPong(timeElapsed / loopDuration, 1f);

        // Interpolate between min and max opacity based on the t value
        float opacity = Mathf.Lerp(minOpacity, maxOpacity, t);

        // Set the new color with the updated opacity (keeping the original RGB values)
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, opacity);
        objectMaterial.color = newColor;
    }
}
