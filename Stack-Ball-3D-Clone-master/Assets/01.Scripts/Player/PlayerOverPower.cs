using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerOverPower : PlayerComponentBase
{
    [SerializeField] private GameObject _fireEffect;
    
    [SerializeField] private float _overPowerDecrease = .3f;

    [SerializeField] private float _downIncrease = .3f;
    [SerializeField] private float _downDecrease = .1f;
    
    public bool IsOverPower{ get; private set; }

    private float _overPower;

    public float OverPower => _overPower;

    public event Action<float> OnOverPower;

    private void Start()
    {
        PlayerAgent.EpisodeBeginAction += OnEpisodeBeginAction;
    }
    
    private void OnEpisodeBeginAction()
    {
        _overPower = 0f;
        OnOverPower?.Invoke(0f);
    }

    private void Update()
    {
        OverPowerCheck();
    }
    
    void OverPowerCheck()
    {
        if(IsOverPower)
        {
            _overPower -= Time.deltaTime * _overPowerDecrease;
            if(!_fireEffect.activeInHierarchy)
                _fireEffect.SetActive(true);
        }
        else
        {
            if(_fireEffect.activeInHierarchy)
                _fireEffect.SetActive(false);
    
            if(PlayerAgent.IsDown)
                _overPower += Time.deltaTime * _downIncrease;
            else
                _overPower -= Time.deltaTime * _downDecrease;
        }
        
        OnOverPower?.Invoke(_overPower);

        if(_overPower >= 1)
        {
            IsOverPower = true;
            _overPower = 1f;
        }
        else if(_overPower <= 0)
        {
            IsOverPower = false;
            _overPower = 0f;
        }

        _overPower = Mathf.Clamp01(_overPower);
    }

}
