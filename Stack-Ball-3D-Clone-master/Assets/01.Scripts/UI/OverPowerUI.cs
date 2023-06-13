using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverPowerUI : MonoBehaviour
{
    [SerializeField] private PlayerAgent _playerAgent;
    [SerializeField] private SpriteRenderer _overPowerFill;

    private PlayerOverPower _playerOverPower;
    private Material _overPowerMat;
    private readonly int ARC1_ID = Shader.PropertyToID("_Arc3");
    
    private void Start()
    {
        _playerOverPower = _playerAgent.ComponentController.GetComponent<PlayerOverPower>();

        _overPowerMat = _overPowerFill.material;
        _playerOverPower.OnOverPower += OnOverPower;
    }
    private void OnOverPower(float overPower)
    {
        // UI
        if(overPower >= 0.3f || _overPowerFill.color == Color.red)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    
        if(overPower >= 1)
        {
            _overPowerFill.color = Color.red;
        }
        else if(overPower <= 0)
        {
            _overPowerFill.color = Color.white;
        }
    
        if(gameObject.activeInHierarchy)
            _overPowerMat.SetFloat(ARC1_ID, overPower * 360f);
    }


}
