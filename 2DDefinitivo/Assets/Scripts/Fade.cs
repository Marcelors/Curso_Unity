﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject painelFume;
    public Image fume;
    public Color[] CorTransicao;
    public float step;

    private bool emTransicao;
    private void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        if (emTransicao == false)
        {
            painelFume.SetActive(true);

            StartCoroutine("fadeInRotine");
        }

    }

    public void FadeOut()
    {
        StartCoroutine("fadeOutRotine");
    }

    IEnumerator fadeInRotine()
    {
        emTransicao = true;
        for (float i = 0; i <= 1; i += step)
        {
            fume.color = Color.Lerp(CorTransicao[0], CorTransicao[1], i);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator fadeOutRotine()
    {
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; i <= 1; i += step)
        {
            fume.color = Color.Lerp(CorTransicao[1], CorTransicao[0], i);
            yield return new WaitForEndOfFrame();
        }
        painelFume.SetActive(false);
        emTransicao = false;
    }
}
