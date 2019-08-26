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
    // Start is called before the first frame update
    void Start()
    {
        mola = GetComponent<SpringJoint2D>();
        estilingue = mola.connectedBody.transform;
        esticadaMaximaQuadrada = EsticadaMaxima * EsticadaMaxima;
        raioParaMouse = new Ray(estilingue.position, Vector3.zero);
        rb2D = GetComponent<Rigidbody2D>();
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

        transform.position = new Vector3(posicaoMouseMundo.x, posicaoMouseMundo.y, transform.position.z);
    }
}
