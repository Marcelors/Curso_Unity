using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public Vector3 Direcao;

    public float Velocidade;

    public GameObject ParticulaBlocos;
    public ParticleSystem ParticulaFolhas;
    public LineRenderer Guia;
    public int PontosDaGuia = 3;

    // Start is called before the first frame update
    void Start()
    {
        Direcao.Normalize();
        Guia.SetVertexCount(PontosDaGuia);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direcao * Velocidade * Time.deltaTime;
        AtualizarLineRenderer();

    }

    private void AtualizarLineRenderer()
    {
        int pontoAtual = 1;

        Vector3 direcaoAtual = Direcao;
        Vector3 ultimaPosicao = transform.position;
        Guia.SetPosition(0, ultimaPosicao);

        while (pontoAtual < PontosDaGuia)
        {
            RaycastHit2D hit = Physics2D.Raycast(ultimaPosicao, direcaoAtual);
            ultimaPosicao = hit.point;
            Guia.SetPosition(pontoAtual, ultimaPosicao);
            direcaoAtual = Vector3.Reflect(direcaoAtual, hit.normal);
            ultimaPosicao += direcaoAtual * 0.15f;
            pontoAtual++;
        }

    }

    private void OnCollisionEnter2D(Collision2D colisor)
    {
        Vector2 normal = colisor.contacts[0].normal;

        Plataforma plataforma = colisor.transform.GetComponent<Plataforma>();
        GeradorDeArestas geradorDeArestas = colisor.transform.GetComponent<GeradorDeArestas>();

        if (plataforma != null)
        {
            if (normal != Vector2.up)
            {
                GerenciadorDoGame.Instance.FinalizarJogo();
                return;
            }

            ParticulaFolhas.transform.position = colisor.transform.position;
            ParticulaFolhas.Play();
        }
        else if (geradorDeArestas != null)
        {
            if (normal == Vector2.up)
            {
                GerenciadorDoGame.Instance.FinalizarJogo();
                return;
            }
        }
        else
        {
            Bounds bordasColisor = colisor.transform.GetComponent<SpriteRenderer>().bounds;
            Vector3 posicao = new Vector3(colisor.transform.position.x + bordasColisor.extents.x, colisor.transform.position.y - bordasColisor.extents.y, colisor.transform.position.z);
            GameObject particulas = (GameObject)Instantiate(ParticulaBlocos, posicao, Quaternion.identity);
            ParticleSystem componentesParticulas = particulas.GetComponent<ParticleSystem>();
            Destroy(particulas, componentesParticulas.duration + componentesParticulas.startLifetime);

            GerenciadorDoGame.numeroDeBlocosDestruidos++;

            Destroy(colisor.gameObject);
        }



        Direcao = Vector2.Reflect(Direcao, normal);
        Direcao.Normalize();
    }
}
