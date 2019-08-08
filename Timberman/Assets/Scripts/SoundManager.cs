using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource Effect;

    public AudioClip FxCut;
    public AudioClip FxDead;
    public AudioClip FxPlay;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip audio)
    {
        Effect.clip = audio;
        Effect.Play();
    }
}
