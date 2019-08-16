using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Principal : MonoBehaviour
{
    public GameObject Jogador;
    public GameObject FelpudoIdle;
    public GameObject FelpudoBate;

    public GameObject Barril;
    public GameObject InimigoEsq;
    public GameObject InimigoDir;

    public GameObject Barra;

    private bool left = true;

    public List<GameObject> ListaBlocos = new List<GameObject>();

    public Text Score;

    int score = 0;

    string text = "Score: {0}";

    bool comecou;
    bool acabou;

    public AudioClip somBate;
    public AudioClip somPerde;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        FelpudoBate.SetActive(false);

        ListaBlocos = new List<GameObject>();
        CriaBarrisInicio();

        Score.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 100);
        Score.text = "TOQUE PARA INICIAR!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!acabou)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!comecou)
                {
                    comecou = true;
                    Barra.SendMessage("Comecou");
                }

                AudioSource.PlayOneShot(somBate);

                if ((Input.mousePosition.x > Screen.width / 2))
                {
                    left = false;
                    bate();
                }
                else
                {
                    left = true;
                    bate();
                }
                ListaBlocos.RemoveAt(0);
                ConfereJogada();
                ReposicionaBlocos();
            }
        }
    }

    void bate()
    {
        FelpudoIdle.SetActive(false);
        FelpudoBate.SetActive(true);

        if(left && Jogador.transform.position.x > 1)
        {
            Jogador.transform.position = new Vector3(Jogador.transform.position.x * -1, Jogador.transform.position.y, Jogador.transform.position.z);
            Jogador.transform.localScale = new Vector3(Jogador.transform.localScale.x * -1, Jogador.transform.localScale.y, Jogador.transform.localScale.z);
        }  else if (!left && Jogador.transform.position.x < -1)
        {
            Jogador.transform.position = new Vector3(Jogador.transform.position.x * -1, Jogador.transform.position.y, Jogador.transform.position.z);
            Jogador.transform.localScale = new Vector3(Jogador.transform.localScale.x * -1, Jogador.transform.localScale.y, Jogador.transform.localScale.z);
        }
        
        Invoke("VoltaAnimacao", 0.25f);

        if (left)
        {
            ListaBlocos[0].SendMessage("BateEsquerda");
        } else
        {
            ListaBlocos[0].SendMessage("BateDireita");
        }

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
            ListaBlocos.Add(CriaNovoBarril(new Vector2(0, -2f+(i * 1.22f))));
        }
    }

    void ReposicionaBlocos()
    {
        GameObject barril = CriaNovoBarril(new Vector2(0, -2f + (7 * 1.22f)));
        ListaBlocos.Add(barril);

        for (int i = 0; i < 7; i++)
        {
            ListaBlocos[i].transform.position = new Vector2(ListaBlocos[i].transform.position.x,
                                                            ListaBlocos[i].transform.position.y - 1.22f);
        }
    }

    void ConfereJogada()
    {
        if (ListaBlocos[0].gameObject.CompareTag("Inimigo"))
        {
            if((ListaBlocos[0].gameObject.name == "inimigoEsq(Clone)" && left) 
                || (ListaBlocos[0].gameObject.name == "inimigoDir(Clone)" && !left))
            {
                FimDeJogo();
            } else
            {
                MarcaPonto();
            }
        } else
        {
            MarcaPonto();
        }
    }

    void MarcaPonto()
    {
        score++;
        Score.text = score.ToString();
        Score.fontSize = 80;
        Score.color = new Color(0.95f, 1f, 0.35f);
        Barra.SendMessage("AumentaBarra");
    }

    void FimDeJogo()
    {

        acabou = true;
        FelpudoBate.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        FelpudoIdle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

        Jogador.GetComponent<Rigidbody2D>().isKinematic = false;
        Jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 3f);
        Jogador.GetComponent<Rigidbody2D>().AddTorque(100f);

        if (left)
        {
            Jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 3f);
            Jogador.GetComponent<Rigidbody2D>().AddTorque(100f);
        } else
        {
            Jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 3f);
            Jogador.GetComponent<Rigidbody2D>().AddTorque(-100f);
        }
        AudioSource.PlayOneShot(somPerde);
        Invoke("RecarregaCena", 2);
    }

    void RecarregaCena()
    {
        SceneManager.LoadScene(0);
    }
}
