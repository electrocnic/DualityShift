using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform pfBullet;
    [SerializeField] private float maxHorizontalMovementForce = 100f;
    [SerializeField] private float maxVerticalMovementForce = 100f;
    [SerializeField] private float horizontalForceMultiplier = 10f;
    [SerializeField] private float verticalForceMultiplier = 10f;

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
        var targetPos = (Vector2)target.position + new Vector2(5f, 5f);
        var dpos = targetPos - rb.position;
        rb.AddForce(new Vector2(Math.Clamp(dpos.x * horizontalForceMultiplier, -maxHorizontalMovementForce, maxHorizontalMovementForce), Math.Clamp(dpos.y * verticalForceMultiplier, -maxVerticalMovementForce, maxVerticalMovementForce)));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Destroy(gameObject);
    }
}