using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class DualityModeController : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            world2.SetActive(true);
            world1.SetActive(false);
        } else {
            world1.SetActive(true);
            world2.SetActive(false);
        }
    }
}
