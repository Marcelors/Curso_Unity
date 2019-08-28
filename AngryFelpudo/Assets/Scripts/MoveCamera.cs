using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform projetil;
    public Transform LimiteEsquerdo;
    public Transform LimiteDireito;

    Vector3 novaPosicao;

    // Update is called once per frame
    void Update()
    {
        novaPosicao = transform.position;

        novaPosicao.x = projetil.position.x;
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, LimiteEsquerdo.position.x, LimiteDireito.position.x);

        transform.position = novaPosicao;
    }
}
