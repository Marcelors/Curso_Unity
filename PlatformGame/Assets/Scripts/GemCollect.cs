using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{

    public AudioClip fxCollect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Score++;
            SoundManager.Instance.PlayerFxGemCollection(fxCollect);
            Destroy(gameObject);
        }
    }
}
