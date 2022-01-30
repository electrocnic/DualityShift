using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    [SerializeReference] private CharacterController2d m_CharacterController2d;
    [SerializeField] private float m_ValuePerAddedPotion = 0.175f;
    private GameObject player;

    [SerializeField] private PotionEffect m_PotionEffect = PotionEffect.Dark;
    

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

        switch (m_PotionEffect) {
            case PotionEffect.Dark:
                m_CharacterController2d.setPotionFillStatus(MathF.Min(1.0f,m_CharacterController2d.getPotionFillStatus() + m_ValuePerAddedPotion));
                break;
            case PotionEffect.Invincible:
                Debug.Log("Making invincible: " + m_ValuePerAddedPotion);
                m_CharacterController2d.MakeInvincible(m_ValuePerAddedPotion);
                break;
        }
    }
}

internal enum PotionEffect {
    Dark,
    Invincible,
}
