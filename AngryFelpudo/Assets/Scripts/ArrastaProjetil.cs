using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastaProjetil : MonoBehaviour
{
    bool clicou;
    public float EsticadaMaxima = 3f;
    float esticadaMaximaQuadrada;
    SpringJoint2D mola;
    Transform estilingue;
    Ray raioParaMouse;
    Rigidbody2D rb2D;
    Vector2 velocidadeAnterior;

    public LineRenderer linhaFrente;
    public LineRenderer linhaTras;
    Ray raioEstilingueFrente;

    CircleCollider2D collider2D;
    float medidaCirculo;
    // Start is called before the first frame update

    private void Awake()
    {
        mola = GetComponent<SpringJoint2D>();
        rb2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        estilingue = mola.connectedBody.transform;
        esticadaMaximaQuadrada = EsticadaMaxima * EsticadaMaxima;
        raioParaMouse = new Ray(estilingue.position, Vector3.zero);
        raioEstilingueFrente = new Ray(linhaFrente.transform.position, Vector3.zero);

        medidaCirculo = collider2D.radius;

        ConfiguraLinha();
    }

    void ConfiguraLinha()
    {
        linhaFrente.SetPosition(0, linhaFrente.transform.position + Vector3.forward * -0.03f);
        linhaTras.SetPosition(0, linhaTras.transform.position + Vector3.forward * -0.01f);

        linhaFrente.sortingLayerName = "Frente";
        linhaTras.sortingLayerName = "Frente";

        linhaFrente.sortingOrder = 3;
        linhaTras.sortingOrder = 1;
    }

    void AtualizaLinha()
    {
        Vector2 estilingueParaProgetil = transform.position - linhaFrente.transform.position;
        raioEstilingueFrente.direction = estilingueParaProgetil;

        Vector3 pontoAmarra = raioEstilingueFrente.GetPoint(estilingueParaProgetil.magnitude + medidaCirculo);

        pontoAmarra.z = -0.03f;
        linhaFrente.SetPosition(1, pontoAmarra);
        pontoAmarra.z = -0.01f;
        linhaTras.SetPosition(1, pontoAmarra);
    }

    // Update is called once per frame
    void Update()
    {
        if (clicou)
        {
            Arrastar();
        }

        if(mola != null)
        {
            if (!rb2D.isKinematic && velocidadeAnterior.sqrMagnitude > rb2D.velocity.sqrMagnitude)
            {
                Destroy(mola);
                rb2D.velocity = velocidadeAnterior;
            }

            if (!clicou)
            {
                velocidadeAnterior = rb2D.velocity;
            }

            AtualizaLinha();
        } else
        {

        }

    }

    private void OnMouseDown()
    {
        clicou = true;
        mola.enabled = false;
    }

    private void OnMouseUp()
    {
        clicou = false;
        mola.enabled = true;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    void Arrastar()
    {
        Vector3 posicaoMouseMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 estilingueParaMouse = posicaoMouseMundo - estilingue.position;

        if(estilingueParaMouse.sqrMagnitude > esticadaMaximaQuadrada)
        {
            raioParaMouse.direction = estilingueParaMouse;
            posicaoMouseMundo = raioParaMouse.GetPoint(EsticadaMaxima);
        }

        posicaoMouseMundo.z = -0.02f;
        transform.position = new Vector3(posicaoMouseMundo.x, posicaoMouseMundo.y, transform.position.z);
    
    }
}
