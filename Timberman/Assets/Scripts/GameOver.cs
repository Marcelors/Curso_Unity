using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text Best;
    // Start is called before the first frame update
    void Start()
    {
        Best.text = PlayerPrefs.GetInt("Best").ToString();
        Score.text =PlayerPrefs.GetInt("Score").ToString();
    }

    public void Restart(){
        SoundManager.Instance.PlaySound(SoundManager.Instance.FxPlay);
        SceneManager.LoadScene("Game");
    }
}
