using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFundo : MonoBehaviour
{
    // Start is called before the first frame update
    private float larguraTela;
    private float alturaTela;

    void Start()
    {
        SpriteRenderer grafico = GetComponent<SpriteRenderer>();

        float larguraImagem = grafico.sprite.bounds.size.x;
        float alturaImagem = grafico.sprite.bounds.size.y;

        alturaTela = Camera.main.orthographicSize *2f;
        larguraTela = alturaTela / Screen.height * Screen.width;

        Vector2 novaEscala = transform.localScale;

        novaEscala.x = larguraTela / larguraImagem +0.25f;
        novaEscala.y = alturaTela / alturaImagem;

        transform.localScale = novaEscala;

        if(this.name == "ImagemFundoB")
        {
            transform.position = new Vector2(larguraTela, 0);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -larguraTela)
        {
            transform.position = new Vector2(larguraTela, 0);
        }
    }
}
