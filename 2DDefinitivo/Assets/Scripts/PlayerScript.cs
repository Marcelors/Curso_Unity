using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRB2D;

    public Transform GroundCheck;
    public float Speed;
    public float JumpForce;

    public bool Grounded;
    public bool Attacking;
    public bool LookLeft;
    public int IdAnimation;

    private float h, v;

    public Collider2D Standing, Crounching;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h > 0 && LookLeft && !Attacking)
        {
            Flip();
        }
        else if (h < 0 && !LookLeft && !Attacking)
        {
            Flip();
        }


        if (v < 0)
        {
            IdAnimation = 2;
            if (Grounded)
            {
                h = 0;
            }

        }
        else if (!h.Equals(0))
        {
            IdAnimation = 1;
        }
        else
        {
            IdAnimation = 0;
        }

        if (Input.GetButtonDown("Fire1") && v >= 0 && !Attacking)
        {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Jump") && Grounded && !Attacking)
        {
            playerRB2D.AddForce(new Vector2(0, JumpForce));
        }

        if (Attacking && Grounded)
        {
            h = 0;
        }

        if(v < 0 && Grounded)
        {
            Crounching.enabled = true;
            Standing.enabled = false;
        } else
        {
            Crounching.enabled = false;
            Standing.enabled = true;
        }

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", IdAnimation);
        playerAnimator.SetFloat("speedY", playerRB2D.velocity.y);
    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.02f);
        playerRB2D.velocity = new Vector2(h * Speed, playerRB2D.velocity.y);
    }

    private void Flip()
    {
        LookLeft = !LookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Atack(int atk)
    {
        switch (atk)
        {
            case 0:
                Attacking = false;
                break;
            case 1:
                Attacking = true;
                break;
        }
    }
}
