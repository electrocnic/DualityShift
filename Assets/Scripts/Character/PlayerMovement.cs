using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;

    public float horizontalMove = 0f;
    private float accel = 1f;
    private bool jump = false;

    private bool swapDirection = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        if (dir < 0 && horizontalMove > 0)
        {
            swapDirection = true;
        }
        else if (dir > 0 && horizontalMove < 0)
        {
            swapDirection = true;
        }
        horizontalMove += dir * accel;

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (swapDirection)
        {
            horizontalMove *= 0.8f;
            swapDirection = false;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}