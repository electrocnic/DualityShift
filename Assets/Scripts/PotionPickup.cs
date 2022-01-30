using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    [SerializeReference] private CharacterController2d m_CharacterController2d;
    [SerializeField] private float m_ValuePerAddedPotion = 0.175f;

    [SerializeField] private PotionEffect m_PotionEffect = PotionEffect.Dark;
    

    private void Start()
    {
        m_CharacterController2d = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ReferenceEquals(other.gameObject, m_CharacterController2d.gameObject))
        {
            return;
        }
        Destroy(gameObject);

        switch (m_PotionEffect) {
            case PotionEffect.Dark:
                m_CharacterController2d.setPotionFillStatus(MathF.Min(1.0f,m_CharacterController2d.getPotionFillStatus() + m_ValuePerAddedPotion));
                break;
            case PotionEffect.Invincible:
                m_CharacterController2d.MakeInvincible(m_ValuePerAddedPotion);
                break;
        }
    }
}

internal enum PotionEffect {
    Dark,
    Invincible,
}
