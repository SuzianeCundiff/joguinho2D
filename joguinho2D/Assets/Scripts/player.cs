 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rigid;
    private Animator animator;

    bool isBlowing; // by default is false

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        /* move o personagem de acordo com a posição
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        
        */

        float movement = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2 (movement * speed, rigid.velocity.y);

        if(movement > 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(movement < 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(movement == 0f)
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing) //isJumping precisa ser falso.
        {
            if(!isJumping)
            {
                rigid.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                animator.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    animator.SetBool("doubleJump", true);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // layer Ground
        {
            isJumping = false;
            doubleJump = false;
            animator.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Saw") // tag Spike or Saw
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // layer Ground
        {
            isJumping = true;
        }  
    }

    void  OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }       
    }

}
