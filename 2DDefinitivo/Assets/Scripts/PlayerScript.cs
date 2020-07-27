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

    public Transform Hand;
    public LayerMask Interaction;
    public GameObject InteractionObject;
    private Vector3 dir = Vector3.right;

    public Collider2D Standing, Crounching;

    public GameObject[] Weapons;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB2D = GetComponent<Rigidbody2D>();

        foreach (var weapon in Weapons)
        {
            weapon.SetActive(false);
        }
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

        if (Input.GetButtonDown("Fire1") && vertical >= 0 && !Attacking && InteractionObject == null)
        {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Fire1") && vertical >= 0 && !Attacking && InteractionObject != null)
        {
            InteractionObject.SendMessage(nameof(ChestScript.Interaction), SendMessageOptions.DontRequireReceiver);
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
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.02f, layerMask: WhatIsGround);
        playerRB2D.velocity = new Vector2(horizontal * Speed, playerRB2D.velocity.y);

        Interact();
    }

    private void Flip()
    {
        LookLeft = !LookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        dir.x = x;
    }

    private void Atack(int atk)
    {
        switch (atk)
        {
            case 0:
                Attacking = false;
                Weapons[2].SetActive(false);
                break;
            case 1:
                Attacking = true;
                break;
        }
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(Hand.position, dir, 0.2f, Interaction);
        Debug.DrawRay(Hand.position, dir * 0.2f, Color.red);
        if (hit == true)
        {
            InteractionObject = hit.collider.gameObject;
        }
        else
        {
            InteractionObject = null;
        }
    }

    private void WeaponController(int id)
    {
        foreach (var weapon in Weapons)
        {
            weapon.SetActive(false);
        }

        Weapons[id].SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coletavel":
                Destroy(collision.gameObject);
                break;
        }
    }
}
