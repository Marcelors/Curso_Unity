using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Fade fade;
    private PlayerScript playerScript;

    public Transform Destino;

    public bool Escuro;
    public Material Luz2D;
    public Material Padrao2D;

    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
    }

    public void Interaction()
    {
        StartCoroutine("acionarPorta");
    }

    IEnumerator acionarPorta()
    {
        fade.FadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        playerScript.gameObject.SetActive(false);
        if (Escuro)
        {
            playerScript.ChangeMaterial(Luz2D);
        } else
        {
            playerScript.ChangeMaterial(Padrao2D);
        }
        playerScript.transform.position = Destino.position;
        playerScript.gameObject.SetActive(true);
        fade.FadeOut();
    }
}
