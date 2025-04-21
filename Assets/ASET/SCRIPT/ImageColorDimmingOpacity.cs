using UnityEngine;
using UnityEngine.UI; // Required for using the Image component

public class ImageColorDimmingOpacity : MonoBehaviour
{
    public Image imageComponent; // Reference to the Image component
    public float loopDuration = 2f; // Time for a full loop (dim and brighten)
    public float minOpacity = 0.2f; // Minimum opacity (fully dimmed)
    public float maxOpacity = 1f;   // Maximum opacity (fully bright)

    private Color baseColor;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        // Get the initial color of the Image component
        baseColor = imageComponent.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate how far along in the loop we are
        timeElapsed += Time.deltaTime;
        float t = Mathf.PingPong(timeElapsed / loopDuration, 1f);

        // Interpolate between min and max opacity based on the t value
        float opacity = Mathf.Lerp(minOpacity, maxOpacity, t);

        // Set the new color with the updated opacity (keeping the original RGB values)
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, opacity);
        imageComponent.color = newColor;
    }
}

