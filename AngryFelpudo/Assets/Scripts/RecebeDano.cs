using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecebeDano : MonoBehaviour
{
    public float PontosDeVida = 2;
    public Sprite ImageMachucado;
    public float VelocidadeParaDano = 5;

    private float pontosDeVidaAtuais;
    private float velocidadePAraDanoQuadrado;
    private SpriteRenderer sRenderer;


    void Start()
    {
        pontosDeVidaAtuais = PontosDeVida;
        velocidadePAraDanoQuadrado = VelocidadeParaDano * VelocidadeParaDano;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (!colisao.gameObject.CompareTag("Area"))
            return;
        if (colisao.relativeVelocity.sqrMagnitude < velocidadePAraDanoQuadrado)
            return;

        sRenderer.sprite = ImageMachucado;
        pontosDeVidaAtuais--;

        if (pontosDeVidaAtuais <= 0)
        {
            Matar();
        }
    }

    private void Matar()
    {
        sRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<ParticleSystem>().Play();
    }
}
