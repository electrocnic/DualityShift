using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Object = System.Object;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10;
    private Vector3 target;
    private GameObject origin;
    private bool alreadyHit = false;

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = origin.GetComponent<Rigidbody2D>().velocity;
        var dir = target - rb.transform.position;
        var dir2 = new Vector2(dir.x, dir.y);
        dir2.Normalize();
        dir = dir2;
        Transform transform1 = transform;
        // transform1.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(transform1.forward, dir, new Vector3(0, 0, 1)));
        transform1.position += dir * 2f;
        rb.AddForce(dir * projectileSpeed);
        Destroy(gameObject, 5f);
    }

    public void Target(Vector3 target)
    {
        this.target = target;
    }

    public void Origin(GameObject origin)
    {
        this.origin = origin;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == origin || alreadyHit)
        {
            return;
        }

        var damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            string enemyType = damageable.objectId;
            alreadyHit = true;
            damageable.Damage(100f);
        }
    }
}
