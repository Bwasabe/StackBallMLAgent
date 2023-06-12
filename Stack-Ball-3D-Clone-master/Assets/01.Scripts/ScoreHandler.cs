using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler instance;
    public int score = 0;
    private Text _scoreText;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = score.ToString();

    }

    void Start()
    {

        AddScore(0);
    }
    

    public void AddScore(int amount)
    {
        score += amount;
        if(score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }

        _scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
