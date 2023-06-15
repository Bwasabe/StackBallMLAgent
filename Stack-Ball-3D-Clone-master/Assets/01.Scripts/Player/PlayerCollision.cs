using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollision : PlayerComponentBase, IWinAble
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Platforms _platforms;
    [SerializeField] private LevelSpawner _levelSpawner;

    [SerializeField] private GameObject _splashEffect;
    [SerializeField] private GameObject _winEffect;

    [SerializeField] private float _bounceSpeed = 250f;

    [SerializeField] private Stage _stage;

    public event Action<float> OnBreakPlatform;

    private int _currentBrokenPlatforms;
    private int _totalPlatforms;

    private Rigidbody _rb;

    private PlayerOverPower _playerOverPower;

    private Vector3 _resetPosition;

    protected override void Awake()
    {
        base.Awake();
        _resetPosition = transform.localPosition;

        PlayerAgent.EpisodeBeginAction += OnEpisodeBegin;
    }
    private void Start()
    {
        _rb = PlayerAgent.ComponentController.GetComponent<Rigidbody>();
        _playerOverPower = PlayerAgent.ComponentController.GetComponent<PlayerOverPower>();
    }

    private void OnEpisodeBegin()
    {
        transform.localPosition = _resetPosition;
        _currentBrokenPlatforms = 0;
        
        StartCoroutine(SetTotalPlatform());
    }

    private IEnumerator SetTotalPlatform()
    {
        yield return null;
        yield return null;
        yield return null;
        _totalPlatforms = _platforms.transform.childCount;
    }


    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.CompareTag("Finish"))
        {
            // Reset
            transform.localPosition = _resetPosition;
            
            GameObject win = Instantiate(_winEffect, _mainCam.transform, true);
            win.transform.localPosition = Vector3.up * 1.5f;
            win.transform.eulerAngles = Vector3.zero;

            ParticleSystem particleSystem = win.GetComponent<ParticleSystem>(); 
            
            Destroy(win, particleSystem.duration);
            _stage.WinGame();
            
            PlayerAgent.AddReward(_levelSpawner.Level);
            return;
        }

        if(!PlayerAgent.IsDown)
        {
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);

            GameObject splash = Instantiate(_splashEffect, target.transform, true);
            splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);

            float randomScale = Random.Range(0.18f, 0.25f);

            splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
            splash.transform.localPosition   =
                new Vector3(transform.localPosition .x, transform.localPosition .y - 0.25f, transform.localPosition .z);

            splash.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
        }
        else
        {
            if(_playerOverPower.IsOverPower)
            {
                if(target.gameObject.CompareTag("GoodPlatform") || target.gameObject.CompareTag("BadPlatform"))
                {
                    PlatformController platformController = target.transform.parent.GetComponent<PlatformController>();
                    platformController.BreakAllPlatforms();
                    
                    
                    _currentBrokenPlatforms = _totalPlatforms - _platforms.transform.childCount;
                    
                    PlayerAgent.AddReward(0.1f);
                    OnBreakPlatform?.Invoke(_currentBrokenPlatforms / (float)_totalPlatforms);

                }
            }
            else
            {
                if(target.gameObject.CompareTag("GoodPlatform"))
                {
                    target.transform.parent.GetComponent<PlatformController>().BreakAllPlatforms();


                    _currentBrokenPlatforms = _totalPlatforms - _platforms.transform.childCount;
                    PlayerAgent.AddReward(0.1f);
                    OnBreakPlatform?.Invoke(_currentBrokenPlatforms / (float)_totalPlatforms);
                }

                if(target.gameObject.CompareTag("BadPlatform"))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    PlayerAgent.AddReward(-5f);
                    
                    PlayerAgent.EndEpisode();
                }
            }
        }
    }

    void OnCollisionStay(Collision target)
    {
        if(!PlayerAgent.IsDown || target.gameObject.CompareTag("Finish"))
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);
    }
    public void WinGame()
    {
        _currentBrokenPlatforms = 0;
        StartCoroutine(SetTotalPlatform());
    }
}
