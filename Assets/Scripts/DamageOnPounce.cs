using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnPounce : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        var damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            var com = transform.position;
            var otherCom = col.transform.position;
            var d = com - otherCom;
            var angle = Vector2.SignedAngle(d, Vector2.right);
            if (angle < 0)
            {
                damageable.Damage(400f);
            }
        }
    }
}
