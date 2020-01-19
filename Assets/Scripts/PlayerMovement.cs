using System.Collections;
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

    Vector3 velocity;
    bool isGrounded;



    private void Start()
    {
        th_OnIdle = new Thread(OnIdle);

    }
    //                                 //
    // Update is called once per frame //
    //                                 //
    void Update()
    {
        // Empty
    }

    void OnIdle()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            OnJump();
        }
        else
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
    void OnJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        for (int i = 0; isGrounded == false; i++)
        {
            if (isGrounded)
            {
                break;
            }
        }
    }
}
