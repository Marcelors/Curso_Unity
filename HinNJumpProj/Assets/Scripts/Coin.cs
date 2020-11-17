using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreGive = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.game.AddScore(scoreGive: scoreGive);

            AudioManager.obj.PlayerCoin();
            FXManager.obj.ShowPop(transform.position);

            UIManager.obj.UpdateScore();

            this.gameObject.SetActive(false);
        }
    }
}
