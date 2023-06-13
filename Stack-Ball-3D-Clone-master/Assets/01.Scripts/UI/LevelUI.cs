using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _levelValue;
    [SerializeField] private PlayerAgent _playerAgent;
    
    // TODO: OnEpisodeBegin
    private void Start ()
    {
        _spriteRenderer.color = _playerAgent.ComponentController.GetComponent<MeshRenderer>().material.color;

        _text.text = $"{(_levelSpawner._level + _levelValue).ToString()}";
    }
}
