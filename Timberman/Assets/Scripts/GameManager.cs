using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public Text Score;
    private int score;

    public Image TimeBar;
    public float BarWidth = 188f;

    private float timeMax = 10f;
    private float timeBonus = 0.115f;
    private float timeActual;

    public GameObject[] Trunks;
    public List<GameObject> ListTrunks;

    private float trunkHeight = 2.43f;
    private float posInitialY = -3.016743f;
    private int maxTrunk = 6;
    private bool trunkWithoutBranch = false;

    public bool GameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        timeActual = timeMax;
        InitializesTrunk();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {
            BarDecrease();
            if (Input.GetButtonDown("Left") || Input.GetButtonDown("Right"))
            {
                CuteTrunk();
                RepositionTrunk();
            }
        }
    }

    void CreateTrunk(int pos)
    {
        GameObject trunk = Instantiate(trunkWithoutBranch ? Trunks[Random.Range(0, Trunks.Length)] : Trunks[0]);
        trunk.transform.localPosition = new Vector3(-0.04987288f, posInitialY + pos * trunkHeight, 0f);
        ListTrunks.Add(trunk);

        trunkWithoutBranch = !trunkWithoutBranch;
    }

    private void InitializesTrunk()
    {
        for (var i = 0; i <= maxTrunk; i++)
        {
            CreateTrunk(i);
        }
    }

    private void CuteTrunk()
    {
     
        Destroy(ListTrunks[0]);
        ListTrunks.RemoveAt(0);
        SoundManager.Instance.PlaySound(SoundManager.Instance.FxCut);
        ScoreSum();
        BarIncrement();
    }

    private void RepositionTrunk()
    {
        for (var i = 0; i < ListTrunks.Count; i++)
        {
            ListTrunks[i].transform.localPosition = new Vector3(-0.04987288f, posInitialY + i * trunkHeight, 0f);
        }

        CreateTrunk(maxTrunk);
    }

    private void ScoreSum()
    {
        score++;
        Score.text = score.ToString();
    }

    private void BarIncrement()
    {
        if (timeActual + timeBonus < timeMax)
        {
            timeActual = timeActual + timeBonus;
        }
    }

    private void BarDecrease()
    {
        timeActual = timeActual - Time.deltaTime;

        float time = timeActual / timeMax;
        float pos = BarWidth - (time * BarWidth);

        TimeBar.transform.localPosition = new Vector3(-pos, TimeBar.transform.localPosition.y, TimeBar.transform.localPosition.z);

        if (timeActual <= 0f)
        {
            GameOver = true;
            SaveScore();     
        }
    }

    public void SaveScore(){
        if (PlayerPrefs.GetInt("Best") < score)
        {
            PlayerPrefs.SetInt("Best", score);
        } 
        PlayerPrefs.SetInt("Score", score);

        SoundManager.Instance.PlaySound(SoundManager.Instance.FxDead);

        Invoke(nameof(GameManager.CallSceneGameOver), 2f);
    }

    public void CallSceneGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Touch()
    {
        if (!GameOver)
        {
            CuteTrunk();
            RepositionTrunk();
        }
    }
}


