using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2;
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;

    public float jumpFrequency = 1f, nextjumpTime;

    private enum MovementState {idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        MovementState state;

        dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * moveSpeed;

        float dirX2 = Input.GetAxisRaw("Horizontal");
        rb2.velocity = new Vector2(dirX2 * 7f, rb2.velocity.y);

        if (Mathf.Abs(dirX) > 0)
        {
            //anim.SetBool("isRunning",true);
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            //anim.SetBool("isRunning", false);
            state = MovementState.idle;
        }

        if (dirX > 0)
            this.GetComponent<SpriteRenderer>().flipX = false;
        else if (dirX < 0)
            this.GetComponent<SpriteRenderer>().flipX = true;

        if(rb2.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb2.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int) state);


    }

    private void FixedUpdate()
    {
        rb2.velocity = new Vector2(dirX,rb2.velocity.y);
    }


 
    
    public void JumpButton()
    {
        if(IsGrounded())
        {
            jumpSoundEffect.Play();
            rb2.velocity = new Vector2(rb2.velocity.y, jumpSpeed);
        }
      
        //rb2.velocity = Vector2.up * jumpSpeed;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
