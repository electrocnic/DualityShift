using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;

    public float horizontalMove = 0f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maxSpeed = 10f;
    public bool jump = false;

    public int jumpCounter = 0;
    private float dir = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
        
        
        if (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (jump == false)
            {
                if (jumpCounter < 1000000) {
                    jumpCounter++;
                } else {
                    jumpCounter = 0;
                }
                
            }
            jump = true;
        } else {
            jump = false;
        }
    }

    private void FixedUpdate()
    {
        bool swapDirection = false;
        if (dir < 0 && horizontalMove > 0)
        {
            swapDirection = true;
        }
        else if (dir > 0 && horizontalMove < 0)
        {
            swapDirection = true;
        }
        if (swapDirection)
        {
            if (controller.IsGrounded) {
                horizontalMove *= 0.6f;
            } else {
                horizontalMove *= 0.9f;
            }
            
        }
        horizontalMove += dir * acceleration;
        horizontalMove = Math.Clamp(horizontalMove, -maxSpeed, maxSpeed);

        if (dir == 0 && controller.IsGrounded)
        {
            horizontalMove *= 0.95f;
            if (Math.Abs(horizontalMove) < 1f)
            {
                horizontalMove = 0f * Math.Sign(horizontalMove);
            }
        }
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, jumpCounter);
        // jump = false;
    }
}