using System;
using UnityEngine;

public class DualityModeController : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] private float potionConsumationSpeed = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        bool leftShiftPressed = Input.GetKeyDown(KeyCode.LeftShift);
        if (leftShiftPressed && controller.getPotionFillStatus() > 0.0f && !world2.activeInHierarchy) {
            world2.SetActive(true);
            world1.SetActive(false);
        } else if (controller.getPotionFillStatus() <= 0.0f || (leftShiftPressed && world2.activeInHierarchy)) {
            world1.SetActive(true);
            world2.SetActive(false);
        }

        if (world2.activeInHierarchy) {
            controller.setPotionFillStatus(MathF.Max(0.0f, controller.getPotionFillStatus() - potionConsumationSpeed));
        }
    }
}
