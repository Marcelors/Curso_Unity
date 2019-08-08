using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    private GameController _gameController;
    private Rigidbody2D slimeRB;
    private Animator slimeAnimator;

    public float Speed;
    public float timeToWalk;

    private int h;
    private bool isLookLeft;

    public GameObject HitBox;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        slimeRB = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();

        StartCoroutine(nameof(SlimeController.SlimeWalk));
    }

    // Update is called once per frame
    void Update()
    {

        if (h > 0 && isLookLeft)
        {
            Flip();
        }
        else if (h < 0 && !isLookLeft)
        {
            Flip();
        }

        slimeRB.velocity = new Vector2(h * Speed, slimeRB.velocity.y);
        if (h != 0)
        {
            slimeAnimator.SetBool("isWalk", true);
        }
        else
        {
            slimeAnimator.SetBool("isWalk", false);
        }


    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        var x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hitBox"))
        {
            h = 0;
            StopCoroutine(nameof(SlimeController.SlimeWalk));
            Destroy(HitBox);
            _gameController.PlaySFX(_gameController.SfxEnemyDead, 0.2f);
            slimeAnimator.SetTrigger("dead");
        }
    }

    IEnumerator SlimeWalk()
    {
        int rand = Random.Range(0, 100);

        if (rand < 33)
        {
            h = -1;
        }
        else if (rand > 33 && rand < 66)
        {
            h = 0;
        }
        else
        {
            h = 1;
        }

        yield return new WaitForSeconds(timeToWalk);
        StartCoroutine(nameof(SlimeController.SlimeWalk));
    }

    private void OnDead()
    {
        Destroy(this.gameObject);
    }
}
