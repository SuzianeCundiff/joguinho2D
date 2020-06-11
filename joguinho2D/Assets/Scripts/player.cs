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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")) //isJumping precisa ser falso.
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // layer Ground
        {
            isJumping = false;
            animator.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Saw") // tag Spike or Saw
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // layer Ground
        {
            isJumping = true;
        }  
    }

}
