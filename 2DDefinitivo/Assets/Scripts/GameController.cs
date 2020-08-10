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

    [Header("Player")]
    public int idPersonagem;
    public int idPersonagemAtual;
    public int vidaMaxima;
    public int idArma;

    [Header("Banco de Personagem")]
    public string[] nomePersonagem;
    public Texture[] spriteSheetName;

    [Header("Banco de Dados Armas")]
    public Sprite[] SpriteArmas1;
    public Sprite[] SpriteArmas2;
    public Sprite[] SpriteArmas3;
    public int[] danoMinArma;
    public int[] danoMaxArma;
    public int[] tipoDano;




    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        string s = Gold.ToString("N0");
        GoldTXT.text = s.Replace(",", ".");
    }
}
