using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float Speed;
    public int Health;
    public Transform WallCheck;
    public Transform GroundCheck;

    private bool facingRight = true;
    private bool tochedWall = false;
    private bool grounded = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tochedWall = Physics2D.Linecast(transform.position, WallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (tochedWall)
        {
            Flip();
        }

        grounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (!grounded)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Speed, rb2D.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        Speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            DamageEnemy();
        }
    }

    private void DamageEnemy()
    {
        Health--;
        StartCoroutine(DamageEffect());

        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageEffect()
    {
        var actualSpeed = Speed;
        Speed = Speed * -1;
        sprite.color = Color.red;

        rb2D.AddForce(new Vector2(0f, 200f));

        yield return new WaitForSeconds(0.1f);

        Speed = actualSpeed;
        sprite.color = Color.white;
    }
}
