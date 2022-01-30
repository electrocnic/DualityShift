using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Object = System.Object;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Transform pfPuff;
    private WorldController m_WorldController;
    private GameObject m_Player;

    private void Start()
    {
        m_WorldController = Resources.FindObjectsOfTypeAll<WorldController>()[0];
        m_Player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0].gameObject;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) {
            if (m_WorldController.Invincible && Object.ReferenceEquals(m_Player, gameObject))
            {
                return;
            }
            var puffRef = Instantiate(pfPuff, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(puffRef.gameObject, 5f);
        }
    }
}
