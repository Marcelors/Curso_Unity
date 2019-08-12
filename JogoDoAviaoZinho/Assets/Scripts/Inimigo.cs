using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Rigidbody2D rb;

    GameObject jogadorFelpudo;
    bool marcouPonto;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-4, 0);

        jogadorFelpudo = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -25.0f)
        {
            Destroy(this.gameObject);
        } else
        {
            if(transform.position.x < jogadorFelpudo.transform.position.x)
            {
                if (!marcouPonto)
                {
                    marcouPonto = true;
                    rb.velocity = new Vector2(-7.5f, 5.0f);
                    rb.isKinematic = false;
                    rb.AddTorque(-50);
                    GetComponent<SpriteRenderer>().color = new Color(1f, 0.75f, 0.75f);
                    jogadorFelpudo.SendMessage("MarcaPonto");
                }

            }
        }
    }
}
