using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Fade fade;

    public Transform tPlayer;
    public Transform Destino;

    public bool Escuro;
    public Material Luz2D;
    public Material Padrao2D;

    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
    }

    public void Interaction()
    {
        StartCoroutine("acionarPorta");
    }

    IEnumerator acionarPorta()
    {
        fade.FadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        tPlayer.gameObject.SetActive(false);
        if (Escuro)
        {
            tPlayer.gameObject.GetComponent<SpriteRenderer>().material = Luz2D;
        } else
        {
            tPlayer.gameObject.GetComponent<SpriteRenderer>().material = Padrao2D;
        }
        tPlayer.position = Destino.position;
        yield return new WaitForSeconds(0.5f);
        tPlayer.gameObject.SetActive(true);
        fade.FadeOut();
    }
}
