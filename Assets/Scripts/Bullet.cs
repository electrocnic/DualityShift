using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position;
        var dir2 = new Vector2(dir.x, dir.y);
        dir2.Normalize();
        dir = dir2;
        Transform transform1 = transform;
        // transform1.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(transform1.forward, dir, new Vector3(0, 0, 1)));
        transform1.position += dir * 1f;
        rb.AddForce(dir * projectileSpeed);
        Destroy(gameObject, 5f);
    }
}
