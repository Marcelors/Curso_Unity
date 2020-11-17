using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movHor = 0f;
    public float speed = 3;

    public bool isGroundFloor = true;
    public bool isGroundFront = true;

    public LayerMask groundLayer;
    public float frontGroundRayDis = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int ScoreGive = 50;

    private RaycastHit2D hit = new RaycastHit2D();

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    private void Update()
    {
        if (Game.game.gamePaused)
        {
            return;
        }

        isGroundFloor = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
                                           new Vector3(movHor, 0, 0), frontGroundRayDis, groundLayer);

        //Vazio
        if (!isGroundFloor)
        {
            movHor *= -1;
        }

        //Parede
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
        {
            movHor *= -1;
        }

        //Outro inimigo
        var hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z), new Vector3(movHor, 0, 0), frontDist);

        if (hit != null && hit.transform != null)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                movHor *= -1;
            }
        }


    }

    private void FixedUpdate()
    {
        if (Game.game.gamePaused)
        {
            return;
        }
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    private void getKilled()
    {
        gameObject.SetActive(false);
        FXManager.obj.ShowPop(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.player.setDamage();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.obj.PlayerEnemyHit();      
            getKilled();
        }
    }
}
