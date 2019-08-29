using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetador : MonoBehaviour
{

    public Rigidbody2D Alvo;
    SpringJoint2D mola;

    public float VelecodadeParada = 0.025f;
    float velocidadeParadaQuadrdo;
    // Start is called before the first frame update
    private void Awake()
    {
        mola = Alvo.GetComponent<SpringJoint2D>();
    }

    void Start()
    {    
        velocidadeParadaQuadrdo = VelecodadeParada * VelecodadeParada;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetar();
        }

        if(Alvo.velocity.sqrMagnitude < velocidadeParadaQuadrdo && mola == null)
        {
            Resetar();
        }
    }

    void Resetar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>() == Alvo)
        {
            Resetar();
        }
    }
}
