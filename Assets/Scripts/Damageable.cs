using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Transform pfPuff;

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) {
            var puffRef = Instantiate(pfPuff, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(puffRef.gameObject, 5f);
        }
    }
}
