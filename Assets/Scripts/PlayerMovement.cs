﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

/*
 * Hello World.
 * 
 * All script made by Winter4T2H.
 * 
 * Warning: This is a re-write from Brackeys' default scrips.
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 6f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float x;
    public float z;

    Vector3 velocity;
    Vector3 SpeedCheck;
    bool isGrounded;


    //                                 //
    // Update is called once per frame //
    //                                 //
    void Update()
    {
        SpeedCheck.y = velocity.y;
        SpeedCheck.z = z * 250;
        SpeedCheck.x = x * 250;


        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        OnIdle();
        GravityF();
        if (Input.GetButton("Jump"))
        {
            velocity = transform.right * x * speed + transform.forward * z * speed;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * 2);
        }
        if (Input.GetButton("Fire3"))
        {
            speed = 24f;
        }
        else
        {
            speed = 12f;
        }
    }

    void GravityF()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.z = 0f;
            velocity.x = 0f;
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



    void OnIdle()
    {
        if (isGrounded == true)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}