using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeArestas : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var cam = Camera.main;
        var colisor = GetComponent<EdgeCollider2D>();

        Vector2 cantoInferiorEsquerdo = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 cantoSuperiorEsquerdo = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 cantoSuperiorDireito = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 cantoInferiorDireito = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        colisor.points = new Vector2[5] {cantoInferiorEsquerdo, cantoSuperiorEsquerdo, cantoSuperiorDireito, cantoInferiorDireito, cantoInferiorEsquerdo};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
