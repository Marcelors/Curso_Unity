using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private bool sideLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameOver)
        {
            if (Input.GetButtonDown("Left"))
            {
                Flip(true);
                anim.Play("Cut");
            }
            else if (Input.GetButtonDown("Right"))
            {
                Flip(false);
                anim.Play("Cut");
            }
        }
        else
        {
            sprite.flipX = false;
            anim.Play("Die");
        }
    }

    public void Touch(bool isLeft)
    {
        if (!GameManager.Instance.GameOver)
        {
            Flip(isLeft);
            anim.Play("Cut");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.GameOver = true;
        GameManager.Instance.SaveScore();
    }

    private void Flip(bool side)
    {
        if (sideLeft != side)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            sprite.flipX = !sprite.flipX;
            sideLeft = side;
        }
    }
}
