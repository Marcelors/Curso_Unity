using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDoGame : MonoBehaviour
{
    public static int numeroTotalDeBlocos;
    public static int numeroDeBlocosDestruidos;

    public Image Estrelas;
    public GameObject CanvasGo;

    public GameObject Bola;
    public Plataforma Plataforma;

    public static GerenciadorDoGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.loadedLevel == 1)
        {
            CanvasGo.SetActive(false);
            numeroDeBlocosDestruidos = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevel == 1)
        {
            if (numeroDeBlocosDestruidos.Equals(numeroTotalDeBlocos))
            {
                FinalizarJogo();
            }
        }
    }

    public void FinalizarJogo()
    {
        CanvasGo.SetActive(true);
        Estrelas.fillAmount = (float)numeroDeBlocosDestruidos / (float)numeroTotalDeBlocos;
        Plataforma.enabled = false;
        Destroy(Bola);
    }

    public void JogarNovamente()
    {
        Application.LoadLevel("Level1");
    }

    public void MenuPrincipal()
    {
        Application.LoadLevel("MenuPrincipal");
    }

    public void FecharAplicativo()
    {
        Application.Quit();
    }
}
