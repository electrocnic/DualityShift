using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform pfBullet;

    private float lastShot = 0f;

    private void FixedUpdate()
    {
        float t = Time.time;
        if (t - lastShot > 0.3f)
        {
            var bullet = Instantiate(pfBullet, transform.position, Quaternion.identity);
            var bullet2 = bullet.GetComponent<Bullet>();
            bullet2.Origin(gameObject);
            bullet2.Target(target.position);
            lastShot = Time.time;
        }

        var rb = GetComponent<Rigidbody2D>();
        if (rb.position.y < 5f + target.position.y)
        {
            rb.AddForce(Vector2.up * 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Destroy(gameObject);
    }
}