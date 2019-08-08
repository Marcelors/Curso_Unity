using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Sprite[] OverlaySprites;
    public Image Overlay;
    public Text TimeHud;
    public Text ScoreHud;

    public float TimeGame;
    public int Score;

    public GameStatus Status;

    public enum GameStatus
    {
        Win,
        Lose,
        Die,
        Play
    }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeGame = 30f;
        Score = 0;
        Status = GameStatus.Play;
        Overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Status.Equals(GameStatus.Play))
        {
            TimeGame -= Time.deltaTime;
            int timeInt = (int)TimeGame;

            if (timeInt >= 0)
            {
                TimeHud.text = $"Time: {timeInt}";
                ScoreHud.text = $"Score: {Score}";
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (Status.Equals(GameStatus.Win))
            {
                if (SceneManager.GetActiveScene().buildIndex.Equals(1))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }

    public void SetOverlay(GameStatus status)
    {
        Status = status;
        Overlay.enabled = true;
        Overlay.sprite = OverlaySprites[(int)status];
    }
}
