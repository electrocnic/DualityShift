using System;
using UnityEditor.Animations;
using UnityEngine;

public class DualityModeController : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] private float potionConsumationSpeed = 0.01f;
    [SerializeField] AnimatorController spriteRainbowCharacter;
    [SerializeField] AnimatorController spriteDarkCharacter;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_SpriteAnimator;
    
    // Start is called before the first frame update
    void Start() {
        m_SpriteRenderer = controller.GetComponent<SpriteRenderer>();
        m_SpriteAnimator = controller.GetComponent<Animator>();
        Debug.Log("m_SpriteRenderer loaded: " + m_SpriteRenderer);
        Debug.Log("m_SpriteAnimator loaded: " + m_SpriteAnimator);
    }

    // Update is called once per frame
    void Update() {
        bool leftShiftPressed = Input.GetKeyDown(KeyCode.LeftShift);
        if (leftShiftPressed && controller.getPotionFillStatus() > 0.0f && !world2.activeInHierarchy) {
            world2.SetActive(true);
            world1.SetActive(false);
            //controller.GetComponent<SpriteRenderer>().sprite = spriteDarkCharacter;
            m_SpriteAnimator.runtimeAnimatorController = spriteDarkCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = true;
        } else if (controller.getPotionFillStatus() <= 0.0f || (leftShiftPressed && world2.activeInHierarchy)) {
            world1.SetActive(true);
            world2.SetActive(false);
            // controller.GetComponent<SpriteRenderer>().sprite = spriteRainbowCharacter;
            m_SpriteAnimator.runtimeAnimatorController = spriteRainbowCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (world2.activeInHierarchy) {
            controller.setPotionFillStatus(MathF.Max(0.0f, controller.getPotionFillStatus() - potionConsumationSpeed));
        }
    }
}
