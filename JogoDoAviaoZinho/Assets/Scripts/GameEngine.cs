using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject Inimigo;
    // Start is called before the first frame update
    void Comecou()
    {
        InvokeRepeating("CriaInimigo", 0f, 1.5f);
    }

    void Acabou()
    {
        CancelInvoke("CriaInimigo");
    }

    private void CriaInimigo()
    {
        float alturaAleatoria = 10.0f * Random.value - 5;

        GameObject novoInimigo = Instantiate(Inimigo);

        novoInimigo.transform.position = new Vector2(15.0f, alturaAleatoria);


    }
}
