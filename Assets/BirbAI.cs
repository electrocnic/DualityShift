using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform pfBullet;
    [SerializeField] private float maxForce = 100f;
    [SerializeField] private float horizontalProporionalForceMultiplier = 10f;
    [SerializeField] private float verticalProportionalForceMultiplier = 10f;
    [SerializeField] private float horizontalDerivativeForceMultiplier = 10f;
    [SerializeField] private float verticalDerivativeForceMultiplier = 10f;
    [SerializeField] private float maxSpeed = 400f;
    private float lastShot = 0f;
    private Vector2 lastDPos = Vector2.zero;

    private void Start()
    {
        lastDPos = target.position - transform.position;
    }

    private void FixedUpdate()
    {
        float t = Time.time;
        if (t - lastShot > 1.4f)
        {
            var bullet = Instantiate(pfBullet, transform.position, Quaternion.identity);
            var bullet2 = bullet.GetComponent<Bullet>();
            bullet2.Origin(gameObject);
            bullet2.Target(target.position);
            lastShot = Time.time;
        }

        var rb = GetComponent<Rigidbody2D>();
        var targetPos = (Vector2) target.position + new Vector2(5f, 5f);
        var dpos = targetPos - rb.position;
        var ddpos = dpos - lastDPos;
        // Debug.Log(ddpos);
        var controlOutput = new Vector2(dpos.x * horizontalProporionalForceMultiplier, dpos.y * verticalProportionalForceMultiplier) + new Vector2(ddpos.x * horizontalDerivativeForceMultiplier, ddpos.y * verticalDerivativeForceMultiplier);
        lastDPos = dpos;
        rb.AddForce(new Vector2(
            Math.Clamp(controlOutput.x, -maxForce, maxForce),
            Math.Clamp(controlOutput.y, -maxForce, maxForce)));
        rb.velocity = new Vector2(Math.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
            Math.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Destroy(gameObject);
    }
}