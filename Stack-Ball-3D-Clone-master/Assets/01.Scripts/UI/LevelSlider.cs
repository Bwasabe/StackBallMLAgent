using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSlider : MonoBehaviour
{
    [SerializeField] private PlayerAgent _playerAgent;
    [SerializeField] private SpriteRenderer _levelProgressBar;
    [SerializeField] private SpriteRenderer _levelProgressBackground;

    private Color _playerColor;
    private Material _progressMaterial;
    
    private readonly int Progress_ID = Shader.PropertyToID("_Progress");

    // TODO: OnEpisodeBegin
    private void Start()
    {
        _playerColor = _playerAgent.ComponentController.GetComponent<MeshRenderer>().material.color;

        PlayerCollision playerCollision = _playerAgent.ComponentController.GetComponent<PlayerCollision>(); 
        
        _progressMaterial = _levelProgressBar.material;

        _levelProgressBar.color = _playerColor + Color.gray;
        _levelProgressBackground.color = _playerColor;

        playerCollision .OnBreakPlatform += LevelProgressFill;
    }
    
    private void LevelProgressFill(float value)
    {
        _progressMaterial.SetFloat(Progress_ID, value);
    }
}
