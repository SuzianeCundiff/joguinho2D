using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    private SpriteRenderer sprtRenderer;
    private CircleCollider2D circCollider;

    public GameObject collected;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        sprtRenderer = GetComponent<SpriteRenderer>();
        circCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            sprtRenderer.enabled = false; //desliga a renderização da maçã
            circCollider.enabled = false; //desliga o circle collider
            collected.SetActive(true); //ativa a animação collected

            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.25f); //destroi o objeto em 0.25seg
        }
    }

}
