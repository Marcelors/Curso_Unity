using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    bool comecou;
    bool acabou;
    Rigidbody2D corpoJogador;
    Vector2 forcaImpulso = new Vector2(0, 500);
    public GameObject ParticulaPena;

    // Start is called before the first frame update
    void Start()
    {
        corpoJogador = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!comecou)
            {
                comecou = true;
                corpoJogador.isKinematic = false;
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
            Destroy(this.gameObject);
        }

        transform.rotation = Quaternion.Euler(0, 0, corpoJogador.velocity.y * 3); 
    }
}
