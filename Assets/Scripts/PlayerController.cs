using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float hareketHizi = 10f;

    Rigidbody2D playerRB;
    Animator AnimatorPlayer;
    

    public float hareket = 0f;

    bool facingRight = true;
    public bool isGrounded = false;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    bool mouseIsNotOverUI;

    public float timeinair = 0;
    public float deathtimer = 5;
    

    [SerializeField] private AudioSource deathSoundEffect;
    
    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        AnimatorPlayer = GetComponent<Animator>();
//        PlayerLife playerLife = FindObjectOfType<PlayerLife>();
        
    }


    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        //AnimatorPlayer.SetTrigger("isGroundedAnim");
        onGroundCheck();

        hareket = Input.GetAxis("Horizontal") * hareketHizi;
        


        if (Input.touchCount > 0 && mouseIsNotOverUI)
        {
            Touch touch = Input.GetTouch(0);

            AnimatorPlayer.SetTrigger("isGroundedAnim");

            //AnimatorPlayer.GetComponent<Animator>().isActiveAndEnabled = true;

            switch (touch.phase)
            {

                case TouchPhase.Stationary:
                    
                    if (touch.position.x < Screen.width / 2 )
                    {
                        //move to the left
                        //FlipFace();
                        hareket = -1;
                        hareket *= hareketHizi;
                        
                    }

                    if (touch.position.x > Screen.width / 2 )
                    {
                        //move to the right
                        //FlipFace();
                        hareket = 1;
                        hareket *= hareketHizi;
                    }
                    break;

                case TouchPhase.Ended:
                    //AnimatorPlayer.GetComponent<Animator>().enabled = false;
                    //stop moving
                    break;
            }
        }

        if (!isGrounded)
        {
            timeinair += Time.deltaTime;
        }
        else
        {
            timeinair = 0;
        }
        if (timeinair >= deathtimer)
        {
            //dead = true;
            //playerLife.Die();
            Die();
            
        }

    }
    void FixedUpdate()
    {
        Vector2 velocity = playerRB.velocity;
        velocity.x = hareket;
        playerRB.velocity = velocity;
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void onGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        //AnimatorPlayer.SetBool("isGroundAnim", isGrounded);
    }

    public void Die()
    {
        //deathSoundEffect.Play();
        //playerRB.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);
        //AnimatorPlayer.SetTrigger("death");
    }

}
