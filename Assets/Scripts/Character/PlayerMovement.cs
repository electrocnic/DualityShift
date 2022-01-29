using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;

    public float horizontalMove = 0f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maxSpeed = 500f;
    private bool jump = false;
    private float dir = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
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
            horizontalMove *= 0.6f;
        }
        horizontalMove += dir * acceleration;
        horizontalMove = Math.Clamp(horizontalMove, -maxSpeed, maxSpeed);

        if (dir == 0)
        {
            horizontalMove *= 0.95f;
            if (Math.Abs(horizontalMove) < 1f)
            {
                horizontalMove = 0f * Math.Sign(horizontalMove);
            }
        }
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}