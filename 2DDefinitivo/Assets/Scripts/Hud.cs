using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private PlayerScript PlayerScript;
    public Image[] HpBar;
    public Sprite Half, Full;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var img in HpBar)
        {
            img.enabled = true;
            img.sprite = Full;
        }
        PlayerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
    }

    // Update is called once per frame
    void Update()
    {
        ControleBarraVida();
    }

    void ControleBarraVida()
    {
        float percVida = (float)PlayerScript.VidaAtual / (float)PlayerScript.VidaMax;

  
        if (percVida == 1)
        {
            foreach (var img in HpBar)
            {
                img.enabled = true;
                img.sprite = Full;
            }
        }
        else if (percVida >= 0.9f)
        {
            HpBar[4].sprite = Half;
        }
        else if (percVida >= 0.8f)
        {
            HpBar[4].enabled = false;
        }
        else if (percVida >= 0.7f)
        {
            HpBar[3].sprite = Half;
        }
        else if (percVida >= 0.6f)
        {
            HpBar[3].enabled = false;
        }
        else if (percVida >= 0.5f)
        {
            HpBar[2].sprite = Half;
        }
        else if (percVida >= 0.4f)
        {
            HpBar[2].enabled = false;
        }
        else if (percVida >= 0.3f)
        {
            HpBar[1].sprite = Half;
        }
        else if (percVida >= 0.2f)
        {
            HpBar[1].enabled = false;
        }
        else if (percVida >= 0.01f)
        {
            HpBar[0].sprite = Half;
        } else
        {
            HpBar[0].enabled = false;
        }
    }
}
