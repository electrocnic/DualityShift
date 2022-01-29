using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        var damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable) {
            damageable.Damage(100f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
