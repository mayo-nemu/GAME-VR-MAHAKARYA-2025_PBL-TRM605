using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrePointHandler : MonoBehaviour
{
    public int OreToAdd = 1;

    public void AddOneOre()
    {
        OrePoint.instance.AddOre(OreToAdd);
    }
}