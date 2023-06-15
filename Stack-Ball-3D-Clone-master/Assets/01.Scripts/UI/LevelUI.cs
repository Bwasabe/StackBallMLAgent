using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour, IWinAble
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _levelValue;
    [SerializeField] private PlayerAgent _playerAgent;

    private MeshRenderer _playerMeshRenderer;
    
    private void Start ()
    {
        _playerAgent.EpisodeBeginAction += OnEpisodeBegin;
        _playerMeshRenderer = _playerAgent.ComponentController.GetComponent<MeshRenderer>();

        _spriteRenderer.color = _playerMeshRenderer.material.color;
        _text.text = $"{(_levelSpawner.Level + _levelValue).ToString()}";
    }
    private void OnEpisodeBegin()
    {
        StartCoroutine(DelayCoroutine());
    }
    
    public void WinGame()
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return null;
        _spriteRenderer.color = _playerMeshRenderer.material.color;

        _text.text = $"{(_levelSpawner.Level + _levelValue).ToString()}";
    }
}
