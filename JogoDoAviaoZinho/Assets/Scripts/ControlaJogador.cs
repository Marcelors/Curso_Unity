using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour
{
    bool comecou;
    bool acabou;
    Rigidbody2D corpoJogador;
    Vector2 forcaImpulso = new Vector2(0, 500);
    public GameObject ParticulaPena;

    GameObject GameEngine;

    public Text Score;
    private const string text = "Score: {0}";
    private const string textInicio = "Toque para Iniciar";
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameEngine = GameObject.FindGameObjectWithTag("GameController");
        corpoJogador = GetComponent<Rigidbody2D>();
        Score.transform.position = new Vector2(Screen.width / 2, Screen.height - 200);
        score = 0;
        Score.text = textInicio;
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
                    corpoJogador.isKinematic = false;
                    Score.text = string.Format(text, score);
                    GameEngine.SendMessage("Comecou");
                }

                corpoJogador.velocity = new Vector2(0, 0);
                corpoJogador.AddForce(forcaImpulso);

                Vector3 posicaoFelpudo = transform.position + new Vector3(0, 1, 0);

                var peninhas = Instantiate(ParticulaPena);
                peninhas.transform.position = posicaoFelpudo;
            }

            float posicaoFelpudoEmPixels = Camera.main.WorldToScreenPoint(transform.position).y;

            if (posicaoFelpudoEmPixels > Screen.height || posicaoFelpudoEmPixels < 0)
            {
                acabou = true;

                GetComponent<Collider2D>().enabled = false;
                corpoJogador.velocity = Vector2.zero;
                corpoJogador.AddForce(new Vector2(-300, 0));
                corpoJogador.AddTorque(300f);

                GetComponent<SpriteRenderer>().color = new Color(1f, 0.75f, 0.75f);
                FimDeJogo();
            }

            transform.rotation = Quaternion.Euler(0, 0, corpoJogador.velocity.y * 3);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!acabou)
        {
            acabou = true;

            GetComponent<Collider2D>().enabled = false;
            corpoJogador.velocity = Vector2.zero;
            corpoJogador.AddForce(new Vector2(-300, 0));
            corpoJogador.AddTorque(300f);

            GetComponent<SpriteRenderer>().color = new Color(1f, 0.75f, 0.75f);
            FimDeJogo();
        }
    }

    public void MarcaPonto()
    {
        score++;
        Score.text = string.Format(text, score);
    }

    private void FimDeJogo()
    {
        GameEngine.SendMessage("Acabou");
        Invoke("RecarregarCena", 2);
    }

    private void RecarregarCena()
    {
        SceneManager.LoadScene(0);
    }
}
