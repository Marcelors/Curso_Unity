using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameController gameController;

    private Animator playerAnimator;
    private Rigidbody2D playerRB2D;
    private SpriteRenderer spriteRenderer;

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

    public int IdArma;
    public int IdArmaAtual;
    public GameObject[] Weapons;

    public int VidaMax;
    public int VidaAtual;

    public GameObject BallomAlerta;

    // Start is called before the first frame update
    void Start()
    {
        VidaAtual = VidaMax;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        playerAnimator = GetComponent<Animator>();
        playerRB2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (var weapon in Weapons)
        {
            weapon.SetActive(false);
        }

        trocarArma(0);
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

    private void LateUpdate()
    {
        if(IdArma != IdArmaAtual)
        {
            trocarArma(IdArma);
        }
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
            BallomAlerta.SetActive(true);
        }
        else
        {
            InteractionObject = null;
            BallomAlerta.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coletavel":
                collision.gameObject.SendMessage("Coletar", SendMessageOptions.DontRequireReceiver);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void ChangeMaterial(Material novoMaterial)
    {
        spriteRenderer.material = novoMaterial;

        foreach (var item in Weapons)
        {
            item.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
    }

    public void trocarArma(int id)
    {
        IdArma = id;
        Weapons[0].GetComponent<SpriteRenderer>().sprite = gameController.SpriteArmas1[IdArma];
        Weapons[0].GetComponent<WeaponInfo>().DamegeMin = gameController.danoMinArma[IdArma];
        Weapons[0].GetComponent<WeaponInfo>().DamegeMax = gameController.danoMaxArma[IdArma];
        Weapons[0].GetComponent<WeaponInfo>().DamegeType = gameController.tipoDano[IdArma];

        Weapons[1].GetComponent<SpriteRenderer>().sprite = gameController.SpriteArmas2[IdArma];
        var weapon1 = Weapons[1].GetComponent<WeaponInfo>();
        weapon1.DamegeMin = gameController.danoMinArma[IdArma];
        weapon1.DamegeMax = gameController.danoMaxArma[IdArma];
        weapon1.DamegeType = gameController.tipoDano[IdArma];

        Weapons[2].GetComponent<SpriteRenderer>().sprite = gameController.SpriteArmas3[IdArma];
        var weapon2 = Weapons[2].GetComponent<WeaponInfo>();
        weapon2.DamegeMin = gameController.danoMinArma[IdArma];
        weapon2.DamegeMax = gameController.danoMaxArma[IdArma];
        weapon2.DamegeType = gameController.tipoDano[IdArma];

        IdArmaAtual = IdArma;
    }
}
