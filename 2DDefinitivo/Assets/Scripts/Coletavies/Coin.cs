using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController gameController;
    public int Valor;

    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void Coletar()
    {
        gameController.Gold += Valor;
        Destroy(this.gameObject);
    }
}
