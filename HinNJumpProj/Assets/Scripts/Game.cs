using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game game;

    public int maxLives = 3;
    public bool gamePaused = false;
    public int score = 0;

    private void Awake()
    {
        game = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        gamePaused = false;
        UIManager.obj.StartGame();
    }

    public void AddScore(int scoreGive)
    {
        score += scoreGive;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        game = null;
    }
}
