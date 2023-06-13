using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private List<IWinAble> _winAbles;

    private void Awake()
    {
        IWinAble[] winAbles = GetComponentsInChildren<IWinAble>();
        _winAbles = new(winAbles.Length);
        
        foreach (IWinAble winAble in winAbles)
        {
            _winAbles.Add(winAble);
        }
    }
    public void WinGame()
    {
        foreach (IWinAble winAble in _winAbles)
        {
            winAble.WinGame();
        }
    }

}
