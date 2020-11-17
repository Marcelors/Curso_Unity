using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager obj;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip gui;
    public AudioClip hit;
    public AudioClip enemyHit;
    public AudioClip win;

    private AudioSource audioSource;

    private void Awake()
    {
        obj = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayerJump()
    {
        PlaySound(jump);
    }

    public void PlayerCoin()
    {
        PlaySound(coin);
    }

    public void PlayerGui()
    {
        PlaySound(gui);
    }

    public void PlayerHit()
    {
        PlaySound(hit);
    }

    public void PlayerEnemyHit()
    {
        PlaySound(enemyHit);
    }

    public void PlayerWin()
    {
        PlaySound(win);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
