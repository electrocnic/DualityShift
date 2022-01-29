using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(":((((");
        Destroy(gameObject);
    }
}
