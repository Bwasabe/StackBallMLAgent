using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreHandler : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private PlayerAgent _playerAgent;

    private PlayerCollision _playerCollision;
    private PlayerOverPower _playerOverPower;
    void Awake()
    {
        _playerAgent.EpisodeBeginAction += OnEpisodeBegin;
    }
    
    private void OnEpisodeBegin()
    {
        AddScore(0);
    }


    void Start()
    {
        _playerCollision = _playerAgent.ComponentController.GetComponent<PlayerCollision>();
        _playerOverPower = _playerAgent.ComponentController.GetComponent<PlayerOverPower>();
        _playerCollision.OnBreakPlatform += OnPlatformBreak;
        AddScore(0);
    }
    
    private void OnPlatformBreak(float overPower)
    {
        if (_playerOverPower.IsOverPower)
        {
            AddScore(2);
        }
        else
        {
            AddScore(1);
        }
    }
    
    // TODO: Reset
    // private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    // {
    //     _scoreText.text = score.ToString();
    // }



    private void AddScore(int amount)
    {
        score += amount;

        _scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
