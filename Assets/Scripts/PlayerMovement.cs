using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

/*
 * Hello World.
 * 
 * 
 * This is move 001 WAYS
 * All script made by Winter4T2H.
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // Varible
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    public bool secondJump;

    // Checks, Masks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Layers
    public float wallDistance = 0.1f;
    public Transform leftWCheck;
    public LayerMask leftWMask;
    public Transform rightWCheck;
    public LayerMask rightWMask;
    Vector3 velocity;

    // Detector
    public bool isGrounded;
    public bool rightWtouch;
    public bool leftWtouch;

    // Global caches

    public float x;
    public float z;
    public float speedcache;
    public float xcache;
    public float zcache;
    //                                 //
    // Start run once on start         //
    //                                 //
    void Start()
    {
        Thread th_Gravity = new Thread(GravityFunc);
        th_Gravity.Start();
    }

    //                                 //
    // Update is called once per frame //
    //                                 //
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        //OriginL: // Vector3 move = transform.right * x + transform.forward * z;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * -2f * gravity);
            Thread th_Jumpss = new Thread(Jumpssss);
            th_Jumpss.Start();
            
        }
        if (Input.GetButtonDown("Jump") && isGrounded != true && secondJump == false)
        {
            secondJump = true;
            velocity.y = Mathf.Sqrt(jumpHeight * 1.5f * -2f * gravity);
        }



        if (Input.GetButton("Fire3"))
        {
            speed = 24f;
        }
        else
        {
            speed = 12f;
        }

        if (isGrounded && velocity.y < 0)
        {
            secondJump = false; // Set secondJump to false
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
            }
        }
    }
    void WallChecks()
    {
        rightWtouch = Physics.CheckSphere(rightWCheck.position, wallDistance, rightWMask);
        leftWtouch = Physics.CheckSphere(leftWCheck.position, wallDistance, leftWMask);
    }
    void Jumpssss()
    {
        speedcache = speed;
        xcache = x;
        zcache = z;
        while (isGrounded != true)
        {
            Vector3 inairmove = new Vector3(50, 0f, 50);
            controller.Move(inairmove * speedcache * Time.deltaTime);
        }
    }
}
