using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrePoint : MonoBehaviour
{
    public static OrePoint instance;

    public int Ore = 0;
    public TextMeshProUGUI OreText;

    public UnityEngine.Events.UnityEvent SpawnOre;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOre(int amount)
    {
        Ore += amount;
        UpdateOreUI();
    }

    public void CouldronSpawner()
    {
        if (Ore == 3)
        {
            SpawnOre.Invoke();
            Ore -= 3;
        }
    }

    private void UpdateOreUI()
    {
        OreText.text = "" + Ore.ToString();
    }
}
