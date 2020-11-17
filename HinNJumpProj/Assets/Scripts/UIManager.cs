using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager obj;

    public Text livesLbl;
    public Text scoreLbl;

    public Transform UIPanel;

    private void Awake()
    {
        obj = this;
    }

    public void UpdateLives()
    {
        livesLbl.text = "" + Player.player.lives;
    }

    public void UpdateScore()
    {
        scoreLbl.text = "" + Game.game.score;
    }

    public void StartGame()
    {
        AudioManager.obj.PlayerGui();
        Game.game.gamePaused = true;

        UIPanel.gameObject.SetActive(true);
    }

    public void HideInitPanel()
    {
        AudioManager.obj.PlayerGui();
        Game.game.gamePaused = false;

        UIPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
