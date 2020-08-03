using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public string[] DamageTypes;
    public GameObject[] FxDano;
    public GameObject FxMorte;

    public int Gold;

    public TextMeshProUGUI GoldTXT;
    //Armazena a quantidade de ouro que coletamos

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string s = Gold.ToString("N0");
        GoldTXT.text = s.Replace(",", ".");
    }
}
