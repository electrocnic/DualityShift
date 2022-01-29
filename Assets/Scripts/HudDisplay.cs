using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudDisplay : MonoBehaviour {
    [SerializeReference] private CharacterController2d m_CharacterController2d;

    private TextMeshProUGUI m_PotionStatusDisplay;
    // Start is called before the first frame update
    void Start() {
        m_PotionStatusDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        m_PotionStatusDisplay.SetText(((int)(m_CharacterController2d.getPotionFillStatus() * 100.0f)).ToString());
    }
}
