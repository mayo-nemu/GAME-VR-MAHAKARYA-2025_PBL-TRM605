using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameAChildren : MonoBehaviour
{
    public GameObject Parent;

    void Start()
    {
        if (Parent != null)
        {
            // Set the Parent as the parent of the current GameObject
            transform.SetParent(Parent.transform);
        }
    }
}
