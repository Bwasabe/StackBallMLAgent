using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Image levelSlider, levelSliderFill;
    public Image currentLevelImg;
    public Image nextLevelImg;
    public GameObject firstUI, inGameUI, finishUI, gameOverUI;
    public GameObject allButtons;
    private bool _buttons;
    private Material _playerMaterial;
    public Text currentLevelText, nextLevelText, finishLevelText, gameOverScoreText, gameOverBestText;
    private Player _player;
    public Button soundButton;
    public Sprite soundOnImg, soundOffImg;

    void Awake()
    {
        _playerMaterial = FindObjectOfType<Player>().GetComponent<MeshRenderer>().material;
        _player = FindObjectOfType<Player>();
        levelSlider.color = _playerMaterial.color;
        levelSliderFill.color = _playerMaterial.color + Color.gray;
        nextLevelImg.color = _playerMaterial.color;
        currentLevelImg.color = _playerMaterial.color;
        soundButton.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
    }

    void Start()
    {
        currentLevelText.text = FindObjectOfType<LevelSpawner>()._level.ToString();
        nextLevelText.text = FindObjectOfType<LevelSpawner>()._level + 1 + "";
    }

    void Update()
    {
        UIManagement();
    }

    private void UIManagement()
    {
        if (_player.playerState == Player.PlayerState.Prepare)
        {
            if (SoundManager.instance._soundPlay && soundButton.GetComponent<Image>().sprite != soundOnImg)
            {
                soundButton.GetComponent<Image>().sprite = soundOnImg;
            }
            else if (!SoundManager.instance._soundPlay && soundButton.GetComponent<Image>().sprite != soundOffImg)
            {
                soundButton.GetComponent<Image>().sprite = soundOffImg;
            }
        }

        if (Input.GetMouseButtonDown(0) && _player.playerState == Player.PlayerState.Prepare)
        {
            _player.playerState = Player.PlayerState.Play;
            firstUI.SetActive(false);
            inGameUI.SetActive(true);
            finishUI.SetActive(false);
            gameOverUI.SetActive(false);
        }

        if (_player.playerState == Player.PlayerState.Finish)
        {
            firstUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(true);
            gameOverUI.SetActive(false);

            finishLevelText.text = "Level " + FindObjectOfType<LevelSpawner>()._level;
        }

        if (_player.playerState == Player.PlayerState.Dead)
        {
            firstUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(false);
            gameOverUI.SetActive(true);

            gameOverScoreText.text = ScoreHandler.instance.score.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("HighScore").ToString();

            if (Input.GetMouseButtonDown(0))
            {
                ScoreHandler.instance.ResetScore();
                // TODO: EndEpisode
                SceneManager.LoadScene(0);
            }
        }
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void Settings()
    {
        _buttons = !_buttons;
        allButtons.SetActive(_buttons);
    }
}
