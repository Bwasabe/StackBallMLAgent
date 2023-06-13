using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerOverPower : PlayerComponentBase
{
    [SerializeField] private GameObject _fireEffect;
    public bool IsOverPower{ get; private set; }

    private float _overPower;

    public event Action<float> OnOverPower;
    
    private void Update()
    {
        OverPowerCheck();
    }
    
    void OverPowerCheck()
    {
        if(IsOverPower)
        {
            _overPower -= Time.deltaTime * .3f;
            if(!_fireEffect.activeInHierarchy)
                _fireEffect.SetActive(true);
        }
        else
        {
            if(_fireEffect.activeInHierarchy)
                _fireEffect.SetActive(false);
    
            if(PlayerAgent.IsDown)
                _overPower += Time.deltaTime * .8f;
            else
                _overPower -= Time.deltaTime * .5f;
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
