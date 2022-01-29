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
            var vel = GetComponent<Rigidbody2D>().velocity;
            var go = Instantiate(pfBullet, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = vel;
        }
    }
}
