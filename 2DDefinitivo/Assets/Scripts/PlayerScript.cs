using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRB2D;

    public Transform GroundCheck;
    public float Speed;
    public float JumpForce;

    public bool Grounded;
    public LayerMask WhatIsGround;

    public bool Attacking;
    public bool LookLeft;
    public int IdAnimation;

    private float horizontal, vertical;

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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0 && LookLeft && !Attacking)
        {
            Flip();
        }
        else if (horizontal < 0 && !LookLeft && !Attacking)
        {
            Flip();
        }


        if (vertical < 0)
        {
            IdAnimation = 2;
            if (Grounded)
            {
                horizontal = 0;
            }

        }
        else if (!horizontal.Equals(0))
        {
            IdAnimation = 1;
        }
        else
        {
            IdAnimation = 0;
        }

        if (Input.GetButtonDown("Fire1") && vertical >= 0 && !Attacking)
        {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Jump") && Grounded && !Attacking)
        {
            playerRB2D.AddForce(new Vector2(0, JumpForce));
        }

        if (Attacking && Grounded)
        {
            horizontal = 0;
        }

        if (vertical < 0 && Grounded)
        {
            Crounching.enabled = true;
            Standing.enabled = false;
        }
        else
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
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.02f, WhatIsGround);
        playerRB2D.velocity = new Vector2(horizontal * Speed, playerRB2D.velocity.y);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

}
