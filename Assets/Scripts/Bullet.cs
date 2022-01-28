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
        var force = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position;
        rb.AddForce(force.normalized * projectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
