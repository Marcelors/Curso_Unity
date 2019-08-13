using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour
{
    public GameObject Jogador;
    public GameObject FelpudoIdle;
    public GameObject FelpudoBate;

    public GameObject Barril;
    public GameObject InimigoEsq;
    public GameObject InimigoDir;

    private bool left = true;

    public List<GameObject> ListaBlocos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FelpudoBate.SetActive(false);

        ListaBlocos = new List<GameObject>();
        CriaBarrisInicio();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if ((Input.mousePosition.x > Screen.width / 2) && left)
            {
                bate();
                left = false;
            }
            else
            {
                left = true;
                bate();
            }
        }
    }

    void bate()
    {
        FelpudoIdle.SetActive(false);
        FelpudoBate.SetActive(true);
        Jogador.transform.position = new Vector3(Jogador.transform.position.x * -1, Jogador.transform.position.y, Jogador.transform.position.z);
        Jogador.transform.localScale = new Vector3(Jogador.transform.localScale.x * -1, Jogador.transform.localScale.y, Jogador.transform.localScale.z);
        Invoke("VoltaAnimacao", 0.25f);
    }

    void VoltaAnimacao()
    {
        FelpudoIdle.SetActive(true);
        FelpudoBate.SetActive(false);
    }

    GameObject CriaNovoBarril(Vector2 posicao)
    {
        GameObject novoBarril;

        if (Random.value > 0.5f || ListaBlocos.Count <3)
        {
            novoBarril = Instantiate(Barril);
        } else
        {
            if (Random.value > 0.5f)
            {
                novoBarril = Instantiate(InimigoEsq);
            } else
            {
                novoBarril = Instantiate(InimigoDir);
            }             
        }

        novoBarril.transform.position = posicao;

        return novoBarril;
    }

    void CriaBarrisInicio()
    {
        for (int i = 0; i < 7; i++)
        {
            ListaBlocos.Add(CriaNovoBarril(new Vector2(0, -2f+(i*0.99f))));
        }
    }
}
