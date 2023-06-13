using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour, IWinAble
{
    [SerializeField] private GameObject _winPrefab;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private Platforms _platforms;

    [SerializeField] private CameraFollow _cameraFollow;

    [SerializeField] private PlayerAgent _playerAgent;

    private GameObject _winPlatform;
    public int Level{ get; private set; } = 1; 
    private int _platformAddition = 7;
    
    private IEnumerator Start()
    {
        yield return null;
        _playerAgent.EpisodeBeginAction += ResetGame;
        ResetGame();
    }

    private void LevelManagement()
    {
        _platforms.PlatformSelection();
        
        float i = 0;
        for (; i > -Level - _platformAddition; i -= 0.5f)
        {
            GameObject normalPlatform = null;
            if (Level <= 40)
                normalPlatform = _platforms.GetSelectedPlatform(0,2);
            else if (Level > 40 && Level <= 80)
                normalPlatform = _platforms.GetSelectedPlatform(1,3);
            else if (Level > 80 && Level <= 140)
                normalPlatform = _platforms.GetSelectedPlatform(2,4);
            else
                normalPlatform = _platforms.GetSelectedPlatform(3,4);

            normalPlatform.transform.position = new Vector3(0, i, 0);
            normalPlatform.transform.eulerAngles = new Vector3(0, i * _rotationSpeed, 0);

            if (Mathf.Abs(i) >= Level * .3f && Mathf.Abs(i) <= Level * .6f) 
            {
                normalPlatform.transform.eulerAngles += Vector3.up * 180;
            }

            normalPlatform.transform.parent = _platforms.transform;
            
        }
        
        
        _winPlatform = Instantiate(_winPrefab);
        
        _cameraFollow.LastPlatform = _winPlatform.transform;
        
        _winPlatform.transform.position = new Vector3(0, i, 0);
        
    }

    private void ResetGame()
    {
        Level = 1;
        _platformAddition = 7;

        StartCoroutine(DelayCoroutine());
    }
    
    // TODO: Reset
    public void WinGame()
    {
        Level++;
        if(Level > 9)
            _platformAddition = 0;
        else
            _platformAddition = 7;

        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return null;
        LevelManagement();
    }
}
