using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnPounce : MonoBehaviour
{
    [SerializeField] private float neededAngle = 20;

    private void OnCollisionEnter2D(Collision2D col) {
        var damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            var com = transform.position;
            var otherCom = col.transform.position;
            var d = com - otherCom;
            var angle = Vector2.SignedAngle(d, Vector2.left);
            // Debug.Log("Angle: " + angle);
            if (angle > neededAngle && angle < (180f - neededAngle))
            {
                var dmg = 50f * col.relativeVelocity.magnitude;
                damageable.Damage(dmg);
            }
        }
    }
}
