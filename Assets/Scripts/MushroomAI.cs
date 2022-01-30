using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MushroomAI : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float movingSpeed = 2f;
    
    private float lastShot = 0f;

    private void Start() {
    }
    
    private void OnCollisionEnter2D(Collision2D col) {
        var damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null && col.gameObject == target.gameObject)
        {
            damageable.Damage(100f);
        }
    }

    private void FixedUpdate() {
        if (!target) {
            return;
        }

        float t = Time.time;
        if (t - lastShot > 1.4f && false) {
            // Todo: oneshot attack animation
        }

        float dir = (target.position.x - transform.position.x < 0) ? -1f : 1f;
        if (dir < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dir * movingSpeed, rb.velocity.y);
        // rb.AddForce(new Vector2(target.position.x, 0));
    }
}
