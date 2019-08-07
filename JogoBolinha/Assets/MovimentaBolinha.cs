using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentaBolinha : MonoBehaviour
{
    private Rigidbody rb;
    public float Velocidade;

    public GameObject ParticulaItem;

    public Text Score;
    public Text Fim;

    private int pontos;

    private const string ScoreText = "Score: {0}";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Fim.enabled = false;

        Score.text = string.Format(ScoreText, pontos);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.AddForce(move * Velocidade);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Instantiate(ParticulaItem, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            pontos++;
            Score.text = string.Format(ScoreText, pontos);

            if(pontos == 16)
            {
                Fim.enabled = true;
            }
        }

    }
}
