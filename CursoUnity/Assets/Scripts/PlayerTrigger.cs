using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    private Player playerScript;
    public AudioClip fxCoin;

    void Start()
    {
        playerScript = GameObject.Find(nameof(Player)).GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerScript.DamagePlayer();
        }

        if (other.CompareTag("Water"))
        {
            playerScript.DamageWater();
        }

        if (other.CompareTag("Coin"))
        {
            SoundManager.Instance.PlaySound(fxCoin);
            Destroy(other.gameObject);
        }
    }
}
