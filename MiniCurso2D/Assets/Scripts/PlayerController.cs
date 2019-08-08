using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;

    public float Speed;
    public float JumpForce;

    public Transform GroundCheck;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    public Transform Mao;
    public GameObject HitBoxPrefab;

    private bool isLookLeft;
    private bool isGrounded;
    private bool isAtack;

    public Color HitColor;
    public Color NohitColor;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _gameController.PlayerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

        var h = Input.GetAxisRaw("Horizontal");

        if (isAtack && isGrounded)
        {
            h = 0;
        }


        if (h > 0 && isLookLeft)
        {
            Flip();
        }
        else if (h < 0 && !isLookLeft)
        {
            Flip();
        }

        var speedY = playerRb.velocity.y;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(new Vector2(0, JumpForce));
            _gameController.PlaySFX(_gameController.SfxJump, 0.5f);
        }

        if (Input.GetButtonDown("Fire1") && !isAtack)
        {
            isAtack = true;
            playerAnimator.SetTrigger("atack");
            _gameController.PlaySFX(_gameController.SfxAtack, 0.5f);
        }

        playerRb.velocity = new Vector2(h * Speed, y: speedY);

        playerAnimator.SetInteger("h", (int)h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isAtack", isAtack);

    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            _gameController.PlaySFX(_gameController.SfxCoin, 0.8f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("damage"))
        {
            _gameController.PlaySFX(_gameController.SfxDamage, 0.5f);
            StartCoroutine(nameof(PlayerController.DamageController));
        }
    }

    IEnumerator DamageController()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");
        playerSpriteRenderer.color = HitColor;

        yield return new WaitForSeconds(0.3f);

        playerSpriteRenderer.color = NohitColor;

        for (int i = 0; i < 5; i++)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        playerSpriteRenderer.color = Color.white;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        var x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    private void onEndActack()
    {
        isAtack = false;
    }

    private void hitBoxAtack()
    {
        GameObject hitBoxTemp = Instantiate(HitBoxPrefab, Mao.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
    }

    private void footStep()
    {
        _gameController.PlaySFX(_gameController.SfxStep[Random.Range(0, _gameController.SfxStep.Length)], 0.5f);
    }
}
