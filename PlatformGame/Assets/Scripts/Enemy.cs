using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;

    public Transform GroundCheck;
    public Transform GroundCheckWall;
    public LayerMask LayerGround;
    public float RadiusCheck; //2.0f

    private bool grounded;
    private bool groundedWall;

    private Rigidbody2D rb2D;
    private Animator anim;

    private bool isVisible;

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


        groundedWall = Physics2D.OverlapCircle(GroundCheckWall.position, RadiusCheck, LayerGround);

        if (groundedWall)
        {
            Flip();
        }

        if (!grounded)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isVisible)
        {
            rb2D.velocity = new Vector2(Speed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    private void OnBecameVisible()
    {
        Invoke(nameof(Enemy.MoveEnemy), 1.5f);
    }

    private void OnBecameInvisible()
    {
        Invoke(nameof(Enemy.StopEnemy), 1.5f);
    }

    void MoveEnemy()
    {
        isVisible = true;
        anim.Play("Run");
    }

    void StopEnemy()
    {
        isVisible = false;
        anim.Play("Idle");
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Speed *= -1;
    }
}
