using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float VelocidadeDeMovimento;
    public float LimiteEmX;

    // Start is called before the first frame update
    void Start()
    {
        LimiteEmX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        float direcaoHorizontalDoMouse = Input.GetAxis("Mouse X"); // -1 = esquerda; 0 = parado; 1 = direita;

        transform.position += Vector3.right * direcaoHorizontalDoMouse * VelocidadeDeMovimento * Time.deltaTime;

        float xAtual = transform.position.x;

        xAtual = Mathf.Clamp(xAtual, -LimiteEmX, LimiteEmX);

        transform.position = new Vector3(xAtual, transform.position.y, transform.position.z);
    }
}
