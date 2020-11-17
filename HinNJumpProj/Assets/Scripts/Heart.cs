using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int scoreGive = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.obj.PlayerCoin();
            Game.game.AddScore(scoreGive: scoreGive);
            Player.player.AddLive();

            FXManager.obj.ShowPop(transform.position);

            UIManager.obj.UpdateScore();
            UIManager.obj.UpdateLives();

            this.gameObject.SetActive(false);
        }
    }
}
