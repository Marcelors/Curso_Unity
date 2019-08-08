using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource fxPlayer;
    public AudioSource fxGemCollection;

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

        DontDestroyOnLoad(gameObject);
    }

    public void PlayerFxPlayer(AudioClip clip)
    {
        fxPlayer.clip = clip;
        fxPlayer.Play();
    }

    public void PlayerFxGemCollection(AudioClip clip)
    {
        fxGemCollection.clip = clip;
        fxGemCollection.Play();
    }
}
