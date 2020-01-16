using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

/*
 * 
 * Just a Hello World here XD
 * Git is not working!!!
 * Test git 001
 * 
 * All script made by Winter4T2H.
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    public bool secondJump;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float wallDistance = 0.1f;
    public Transform leftWCheck;
    public LayerMask leftWMask;
    public Transform rightWCheck;
    public LayerMask rightWMask;

    Vector3 velocity;
    public bool isGrounded;
    public bool rightWtouch;
    public bool leftWtouch;
    // Update is called once per frame

    void Start()
    {
        Thread th_Gravity = new Thread(GravityFunc);
        th_Gravity.Start();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(isGrounded == true)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            Vector3 move = transform.right * x / 3 + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        // Sprint
        if (Input.GetButton("Fire3"))
        {
            speed = 24f;
        }
        else
        {
            speed = 12f;
        }


        // Jump and double jump
        if (Input.GetButtonDown("Jump") /*&& isGrounded*/)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * -2f * gravity);
        }
        if (Input.GetButtonDown("Jump") && isGrounded != true && secondJump == false)
        {
            // secondJump = true;
            velocity.y = Mathf.Sqrt(jumpHeight * 1.5f * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



    void GravityFunc()
    {
        for (int i = 0; i > 1;)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                secondJump = false; // Set secondJump to false
            }
        }
    }
    void WallChecks()
    {
        rightWtouch = Physics.CheckSphere(rightWCheck.position, wallDistance, rightWMask);
        leftWtouch = Physics.CheckSphere(leftWCheck.position, wallDistance, leftWMask);
    }
}
