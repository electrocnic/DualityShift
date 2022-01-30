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
    [SerializeField] private float m_JumpForce = 25.0f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_JumpSlope = 1.3f;							// Amount of force added when the player jumps.
    [SerializeField] private int m_MaxJumpHeight = 20;
    private bool jump = false;
    private float m_JumpHeight = 0;
    private float m_JumpStartHeight = 0;
    
    private float dir = 0f;
    private float jumpStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //keyDown = jump bis key up. key down starting in air = no jump
        if (controller.m_Grounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) {
            jumpStartTime = Time.time;
            controller.m_Grounded = false;
            m_JumpStartHeight = controller.transform.position.y;

            jump = true;
        } else if (controller.m_Grounded) {
            jump = false;
        }

        if (!controller.m_Grounded &&
            !(Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) {
            jump = false;
        }

        if (jump) {
            // If the player should jump...
            m_JumpHeight = controller.transform.position.y - m_JumpStartHeight;
            if ( m_JumpHeight < m_MaxJumpHeight) {
                float t = Time.time - jumpStartTime;
                float offset = MathF.Log(m_JumpForce) / MathF.Log(m_JumpSlope);
                float finalForceAtCurrentTimestamp = MathF.Pow(m_JumpSlope, -(t - offset));
                
                //Debug.Log("finalForceAtCurrentTimestamp: " + finalForceAtCurrentTimestamp + ", timestamp: " + t);
                m_JumpHeight++;
                controller.m_Rigidbody2D.AddForce(new Vector2(0f, finalForceAtCurrentTimestamp));
            }
        }

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
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, false);
        // jump = false;
    }
}