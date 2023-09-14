using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck3D : MonoBehaviour
{
    //[HideInInspector] 
    public bool inTrigger;
    //[HideInInspector] 
    public GameObject objectInTrigger;

    public List<int> acceptableLayerNumbers;

    private void Awake()
    {
        objectInTrigger = null;
    }

    private void Update()
    {
        if (objectInTrigger == null)
        {
            inTrigger = false;
        }
        else
        {
            inTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < acceptableLayerNumbers.Count; i++)
        {
            if (other.gameObject.layer == acceptableLayerNumbers[i])
            {
                objectInTrigger = other.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < acceptableLayerNumbers.Count; i++)
        {
            if (other.gameObject.layer == acceptableLayerNumbers[i])
            {
                objectInTrigger = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < acceptableLayerNumbers.Count; i++)
        {
            if (other.gameObject.layer == acceptableLayerNumbers[i])
            {
                objectInTrigger = null;
            }
        }
    }
}
