using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(pfBullet, transform.position, Quaternion.identity);
        }
    }
}
