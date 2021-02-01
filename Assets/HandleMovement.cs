﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMovement : MonoBehaviour
{
    Rigidbody2D rb;
    StateManager states;
    HandleAnimations anim;

    public float acceleration;
    public float airAcceleration;
    public float maxSpeed;
    public float jumpSpeed;
    public float jumpDuration;
    float actualSpeed;
    bool justJumped;
    bool canVariableJump;
    float jmpTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        states = GetComponent<StateManager>();
        anim = GetComponent<HandleAnimations>();
        rb.freezeRotation = true;

    }

    void FixedUpdate()
    {
        if (!states.dontMove)
        {
            HorizontalMovement();
            Jump();

        }
       

    }

    void HorizontalMovement()
    {
        actualSpeed = this.maxSpeed;

        if(states.onGround && !states.currentlyAttacking)
        {
            //rb.AddForce(new Vector2((states.horizontal * actualSpeed) - rb.velocity.x * this.acceleration, 0));
            rb.AddForce(new Vector2((states.horizontal * maxSpeed)-rb.velocity.x, 0));
        }

        if(states.horizontal == 0 && states.onGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);


        }


    }

    void Jump()
    {
        
        if(states.vertical > 0)
        {
            if (!justJumped)
            {
                
                justJumped = true;
                

                if (states.onGround)
                {

                    anim.JumpAnim();
                    rb.velocity = new Vector3(rb.velocity.x, this.jumpSpeed);
                    jmpTimer = 0;
                    canVariableJump = true;
                    states.onGround = false;


                }

            }

            else
            {
                states.onGround = false;
                if (canVariableJump)
                {
                    jmpTimer += Time.deltaTime;

                    if(jmpTimer < this.jumpDuration / 1000)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, this.jumpSpeed);
                        
                    }

                }

            }
        }

        else
        {
            
            justJumped = false;
            
        }

    }

    public void AddVelocityOnCharacter(Vector3 direction, float timer)
    {
        StartCoroutine(AddVelocity(timer, direction));

    }

    IEnumerator AddVelocity(float timer, Vector3 direction)
    {
        float t = 0;

        while(t < timer)
        {

            t += Time.deltaTime;

            rb.AddForce(direction * 2);
            yield return null;
        }

    }

}
