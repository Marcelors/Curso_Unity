using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    private Fade fade;
    public string cenaDestino;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
    }


    public void Interaction()
    {
        StartCoroutine("mudancaCena");
    }

    IEnumerator mudancaCena()
    {
        fade.FadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        SceneManager.LoadScene(cenaDestino);
    }
}
