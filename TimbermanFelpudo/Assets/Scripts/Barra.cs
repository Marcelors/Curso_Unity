using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barra : MonoBehaviour
{
    float escalaBarra;
    bool terminou;
    bool comecou;
    public GameObject cameraCena;
    // Start is called before the first frame update
    void Start()
    {
        comecou = false;
        escalaBarra = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
        {
            if(escalaBarra > 0)
            {
                escalaBarra = escalaBarra - 0.15f * Time.deltaTime;
                this.transform.localScale = new Vector2(escalaBarra, this.transform.localScale.y);
            }    else
            {
                if (!terminou)
                {
                    terminou = true;
                    cameraCena.SendMessage("FimDeJogo");
                }
            }    
        }
    }

    void Comecou()
    {
        comecou = true;
    }

    void AumentaBarra()
    {
        escalaBarra = escalaBarra + 0.035f;
        if (escalaBarra > 1.0f)
        {
            escalaBarra = 1f;
        }
     
        this.transform.localScale = new Vector2(escalaBarra, this.transform.localScale.y);
    }
}
