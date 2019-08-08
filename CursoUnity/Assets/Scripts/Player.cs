using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public int JumpForce;
    public int Health;
    public Transform GroundCheck;

    public bool invunerable = false;
    private bool grounded = false;
    private bool jump = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2D;
    private Animator animator;

    public float AttackRate;
    public Transform SpawnAttack;
    public GameObject AttackPrefab;

    public GameObject Crow;

    public AudioClip fxAttack;
    public AudioClip fxHurt;
    public AudioClip fxJump;


    private float nextAttack = 0;

    private Camera cameraScript;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        cameraScript = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            SoundManager.Instance.PlaySound(fxJump);
            jump = true;
        }

        SetAnimations();

        if (Input.GetButton("Fire1") && grounded && Time.time > nextAttack)
        {
            SoundManager.Instance.PlaySound(fxAttack);
            Attack();
        }

    }

    private void FixedUpdate()
    {


        float move = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(move * Speed, rb2D.velocity.y);

        if ((move < 0 && facingRight) || (move > 0 && !facingRight))
        {
            Flip();
        }

        if (jump)
        {
            rb2D.AddForce(new Vector2(0f, JumpForce));
            jump = false;
        }


    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void SetAnimations()
    {
        animator.SetFloat("ValY", rb2D.velocity.y);
        animator.SetBool("JumpFall", !grounded);
        animator.SetBool("Walk", !rb2D.velocity.x.Equals(0f) && grounded);
    }

    private void Attack()
    {
        animator.SetTrigger("Punch");
        nextAttack = Time.time + AttackRate;

        GameObject cloneAttack = Instantiate(AttackPrefab, SpawnAttack.position, SpawnAttack.rotation);

        if (!facingRight)
        {
            cloneAttack.transform.eulerAngles = new Vector3(180, 0, 180);
        }

    }

    public void DamagePlayer()
    {
        if (!invunerable)
        {
            invunerable = true;
            Health--;
            StartCoroutine(DamageEffect());
            Hud.Instance.RefeshLife(Health);

            SoundManager.Instance.PlaySound(fxHurt);

            if (Health < 1)
            {
                KingDeath();
                Invoke(nameof(Player.ReloadLevel), 3f);
                gameObject.SetActive(false);
            }
        }

    }

    public void DamageWater()
    {
        Health = 0;
        Hud.Instance.RefeshLife(Health);
        SoundManager.Instance.PlaySound(fxHurt);
        KingDeath();
        Invoke(nameof(Player.ReloadLevel), 1.5f);
        gameObject.SetActive(false);
    }

    private IEnumerator DamageEffect()
    {
        cameraScript.ShakeCamera(0.5f, 0.1f);

        for (float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invunerable = false;
    }

    private void KingDeath()
    {
        GameObject cloneCrown = Instantiate(Crow, transform.position, Quaternion.identity);
        Rigidbody2D rd2DCrown = cloneCrown.GetComponent<Rigidbody2D>();

        rd2DCrown.AddForce(Vector3.up * 500);

    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
