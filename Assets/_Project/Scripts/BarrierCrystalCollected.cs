using UnityEngine;
using TMPro;

public class BarrierCrystalCollected : MonoBehaviour
{
    public TextMeshProUGUI crystalPossessed;

    private void Start()
    {
        // Ensure the TextMeshProUGUI component is assigned
        if (crystalPossessed == null)
        {
            crystalPossessed = GetComponent<TextMeshProUGUI>();
        }

        // Update the points UI immediately at the start
        UpdatePointsUI();
    }

    private void Update()
    {
        // Continuously update the points UI with the current points from PointManager
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        // Check if PointManager instance exists and update the UI text
        if (PointManager.instance != null)
        {
            crystalPossessed.text = "" + PointManager.instance.points.ToString();
        }
    }
}
