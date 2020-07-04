using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : MonoBehaviour
{
    private Rigidbody2D rigBody;
    private Animator animation;
    private bool colliding;

    public float speed;
    public Transform rightCollision;
    public Transform leftCollision;
    public Transform topCollision;
    public LayerMask layer;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rigBody.velocity = new Vector2(speed, rigBody.velocity.y);

        colliding = Physics2D.Linecast(rightCollision.position, leftCollision.position, layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed = -speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - topCollision.position.y;

            if(height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speed = 0;
                animation.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rigBody.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.42f);
            }
        }
    }
}
