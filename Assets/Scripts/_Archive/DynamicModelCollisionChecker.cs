using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicModelCollisionChecker : MonoBehaviour
{
    public bool isColliding = false;

    private void Start()
    {
        Collider collider = GetComponent<Collider>();
        if(collider != null && !collider.isTrigger) collider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }
}