using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ObjetoBolinha;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = this.transform.position - ObjetoBolinha.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ObjetoBolinha.transform.position + posicaoInicial;
    }
}
