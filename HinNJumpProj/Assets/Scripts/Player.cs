using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    public int lives = 3;
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isImmune = false;

    public float speed = 5f;
    public float jumpForce = 3f;
    public float movHor;

    public float immuneTimeCnt = 0f;
    public float immuneTime = 0.5f;



    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Game.game.gamePaused)
        {
            movHor = 0f;
            return;
        }

        movHor = Input.GetAxisRaw("Horizontal");

        isMoving = movHor != 0f;

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (isImmune)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            immuneTimeCnt -= Time.deltaTime;

            if(immuneTimeCnt <= 0)
            {
                isImmune = false;
                spriteRenderer.enabled = true;
            }
        }

        animator.SetBool(nameof(isMoving), isMoving);
        animator.SetBool(nameof(isGrounded), isGrounded);
        animator.SetBool(nameof(isImmune), isImmune);

        Flip(movHor);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (!isGrounded)
        {
            return;
        }

        rb.velocity = Vector2.up * jumpForce;
        AudioManager.obj.PlayerJump();
    }

    private void Flip(float _xValue)
    {
        Vector3 currentScale = transform.localScale;

        if(_xValue < 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x) * -1;
        } else if(_xValue > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }

        transform.localScale = currentScale;
    }

    private void OnDestroy()
    {
        player = null;
    }

    private void GoImmune()
    {
        isImmune = true;
        immuneTimeCnt = immuneTime;
    }

    public void setDamage()
    {
        lives--;
        AudioManager.obj.PlayerHit();
        UIManager.obj.UpdateLives();

        GoImmune();

        if (lives <= 0)
        {
            FXManager.obj.ShowPop(transform.position);
            Game.game.GameOver();
        }
    }

    public void AddLive()
    {
        if(lives >= Game.game.maxLives)
        {
            return;
        }

        lives++;
    }

}
