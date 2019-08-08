using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public int JumpForce;

    public Transform GroundCheck;
    public LayerMask LayerGround;
    public float RadiusCheck; //2.0f

    private bool grounded;
    private bool jumping;
    private bool facingRight = true;
    private bool IsAlive = true;
    private bool levelCompleted = false;
    private bool timeIsOver = false;

    private Rigidbody2D rb2D;
    private Animator anim;

    public AudioClip FxWin;
    public AudioClip FxDie;
    public AudioClip FxJump;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, RadiusCheck, LayerGround);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
            if (IsAlive && !levelCompleted)
            {
                SoundManager.Instance.PlayerFxPlayer(FxJump);
            }
        }

        var timer = (int)GameManager.Instance.TimeGame;

        if (timer <= 0 && !timeIsOver)
        {
            timeIsOver = true;
            PlayerDie();
        }

        PlayerAnimations();

    }

    private void FixedUpdate()
    {

        if (IsAlive && !levelCompleted)
        {
            float move = Input.GetAxis("Horizontal");

            rb2D.velocity = new Vector2(move * Speed, rb2D.velocity.y);

            if ((move < 0 && facingRight) || move > 0 && !facingRight)
            {
                Flip();
            }

            if (jumping)
            {
                rb2D.AddForce(new Vector2(0f, JumpForce));
                jumping = false;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }


    }

    private void PlayerAnimations()
    {
        if (levelCompleted)
        {
            anim.Play("Celebrate");
        }
        else if (!IsAlive)
        {
            anim.Play("Die");
        }
        else
        {
            if (grounded && rb2D.velocity.x.Equals(0))
            {
                anim.Play("IdIe");
            }
            else if (grounded && !rb2D.velocity.Equals(0))
            {
                anim.Play("Run");
            }
            else if (!grounded)
            {
                anim.Play("Jump");
            }
        }


    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !levelCompleted)
        {
            PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            levelCompleted = true;
            SoundManager.Instance.PlayerFxPlayer(FxWin);
            Physics2D.IgnoreLayerCollision(9, 10);
        }
    }

    private void PlayerDie()
    {
        IsAlive = false;
        Physics2D.IgnoreLayerCollision(9, 10);
        SoundManager.Instance.PlayerFxPlayer(FxDie);
    }

    private void CelebrateAnimationFinished()
    {
        GameManager.Instance.SetOverlay(GameManager.GameStatus.Win);
    }

    private void DieAnimationFinished()
    {
        if (timeIsOver)
        {
            GameManager.Instance.SetOverlay(GameManager.GameStatus.Lose);
        }
        else
        {
            GameManager.Instance.SetOverlay(GameManager.GameStatus.Die);
        }
    }
}
