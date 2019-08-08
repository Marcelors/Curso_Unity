using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeBlocos : MonoBehaviour
{
    public GameObject[] Blocos;
    public int Linhas;

    // Start is called before the first frame update
    void Start()
    {
        CriarGrupoDeBlocos();
    }

    private void CriarGrupoDeBlocos()
    {
        Bounds limiteDoBloco = Blocos[0].GetComponent<SpriteRenderer>().bounds;

        float larguraDoBloco = limiteDoBloco.size.x;
        float alturaDoBloco = limiteDoBloco.size.y;
        float larguraDaTela, alturaDaTela, multiplicadorDaLargura;
        int colunas;
        ColetarInformacoesBloco(larguraDoBloco, out larguraDaTela, out alturaDaTela, out colunas, out multiplicadorDaLargura);

        GerenciadorDoGame.numeroTotalDeBlocos = colunas * Linhas;

        for (int i = 0; i < Linhas; i++)
        {
            for (int y = 0; y < colunas; y++)
            {
                GameObject blocoAleatorio = Blocos[Random.Range(0, Blocos.Length)];
                GameObject blocoInstanciado = (GameObject)Instantiate(blocoAleatorio);

                blocoInstanciado.transform.position = new Vector3(-(larguraDaTela * 0.5f) + (y * larguraDoBloco * multiplicadorDaLargura), (alturaDaTela * 0.5f) - (i * alturaDoBloco), 0);

                float novaLarguraDoBloco = blocoInstanciado.transform.localScale.x * multiplicadorDaLargura;
                blocoInstanciado.transform.localScale = new Vector3(novaLarguraDoBloco, blocoInstanciado.transform.localScale.y, 1);
            }
        }


    }

    private void ColetarInformacoesBloco(float larguraDoBloco, out float larguraDaTela, out float alturaDaTela, out int colunas, out float multiplicadorDaLargura)
    {
        Camera c = Camera.main;

        larguraDaTela = (c.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) - c.ScreenToWorldPoint(new Vector3(0, 0, 0))).x;
        alturaDaTela = (c.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)) - c.ScreenToWorldPoint(new Vector3(0, 0, 0))).y;

        colunas = (int)(larguraDaTela / larguraDoBloco);


        multiplicadorDaLargura = larguraDaTela / (colunas * larguraDoBloco);
    }
}
