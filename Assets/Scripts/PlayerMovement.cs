using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float Move;
    public float RunningSpeed;
    public float WalkingSpeed;
    public float Jump;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask GroundLayer;
    public float Speed;

    private Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Move * Speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())  // Check if the player is grounded before jumping
        {
            anim.SetBool("Jumping", true);
            rb.AddForce(new Vector2(rb.velocity.x, Jump * 10));
        }
        else
        {
            anim.SetBool("Jumping", false);
        }


        if(Move != 0 && Input.GetButtonDown("Fire3"))
        {
           anim.SetInteger("MovementState", 2);     //MovementState 0 = Idle, 1 = Walking, 2 = Running
           float Speed = RunningSpeed;
        }
        else if(Move != 0)
        {
             anim.SetInteger("MovementState", 1);   
             float Speed = WalkingSpeed; 
        }
        else
        {
            anim.SetInteger("MovementState", 0);
            float Speed = WalkingSpeed;
        }
        
    }



    public bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }


    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         Vector3 normal = other.GetContact(0).normal;
    //         if (normal == Vector3.up)
    //         {
    //             isGrounded = true;
    //         }
            
    //     }
    // }

    // void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }
}
