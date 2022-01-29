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
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && controller.getPotionFillStatus() > 0.0f) {
            world2.SetActive(true);
            world1.SetActive(false);
            controller.setPotionFillStatus(MathF.Max(0.0f, controller.getPotionFillStatus() - potionConsumationSpeed));
        } else {
            world1.SetActive(true);
            world2.SetActive(false);
        }
    }
}
