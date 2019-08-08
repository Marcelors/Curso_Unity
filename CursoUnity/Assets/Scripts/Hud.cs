using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] Sprites;
    public Image Life;

    public static Hud Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefeshLife(int playerHealth)
    {
        Life.sprite = Sprites[playerHealth];
    } 
}
