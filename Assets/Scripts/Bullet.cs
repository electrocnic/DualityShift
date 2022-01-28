using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position;
        dir.Normalize();
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(dir, Vector3.right));
        transform.position += dir * 3f;
        rb.AddForce(dir * projectileSpeed);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
