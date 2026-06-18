using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class BarierHandler : MonoBehaviour
{
    public int requiredPoints = 10;
    public TextMeshProUGUI pointsText; // Reference to the TMP text component

    private void Start()
    {
        // Update the TMP text to display the required points
        pointsText.text = "/" + requiredPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PointManager.instance.points >= requiredPoints)
        {
            gameObject.SetActive(false);
            PointManager.instance.AddPoints(-requiredPoints);
        }
    }
}
