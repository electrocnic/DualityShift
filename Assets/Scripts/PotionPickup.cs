using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    [SerializeReference] private CharacterController2d m_CharacterController2d;
    [SerializeField] private float m_ValuePerAddedPotion = 0.175f;
    private GameObject player;

    private void Start()
    {
        player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0].gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != player)
        {
            return;
        }
        Destroy(gameObject);
        m_CharacterController2d.setPotionFillStatus(MathF.Min(1.0f,m_CharacterController2d.getPotionFillStatus() + m_ValuePerAddedPotion));
    }
}
