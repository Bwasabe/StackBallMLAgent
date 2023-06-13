using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollision : PlayerComponentBase
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Platforms _platforms;

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
        _resetPosition = transform.position;

        PlayerAgent.EpisodeBeginAction += OnEpisodeBegin;
    }
    private void Start()
    {
        _rb = PlayerAgent.ComponentController.GetComponent<Rigidbody>();
        _playerOverPower = PlayerAgent.ComponentController.GetComponent<PlayerOverPower>();
    }

    private void OnEpisodeBegin()
    {
        _totalPlatforms = _platforms.transform.childCount;
        transform.position = _resetPosition;

    }


    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.CompareTag("Finish"))
        {
            // Reset
            transform.position = _resetPosition;
            _stage.WinGame();

            return;
        }

        if(!PlayerAgent.IsDown)
        {
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);

            GameObject splash = Instantiate(_splashEffect, target.transform, true);
            splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);

            float randomScale = Random.Range(0.18f, 0.25f);

            splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
            splash.transform.position =
                new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);

            splash.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
        }
        else
        {
            if(_playerOverPower.IsOverPower)
            {
                if(target.gameObject.CompareTag("GoodPlatform") || target.gameObject.CompareTag("BadPlatform"))
                {
                    target.transform.parent.GetComponent<PlatformController>().BreakAllPlatforms();
                }
            }
            else
            {
                if(target.gameObject.CompareTag("GoodPlatform"))
                {
                    target.transform.parent.GetComponent<PlatformController>().BreakAllPlatforms();

                    _currentBrokenPlatforms++;
                    OnBreakPlatform?.Invoke(_currentBrokenPlatforms / (float)_totalPlatforms);
                }

                if(target.gameObject.CompareTag("BadPlatform"))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    PlayerAgent.EndEpisode();
                }
            }
        }


        if(target.gameObject.CompareTag("Finish"))
        {
            // TODO: Reset
            // PlayerAgent.IncreaseScore();
            GameObject win = Instantiate(_winEffect, _mainCam.transform, true);
            win.transform.localPosition = Vector3.up * 1.5f;
            win.transform.eulerAngles = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision target)
    {
        if(!PlayerAgent.IsDown || target.gameObject.CompareTag("Finish"))
            _rb.velocity = new Vector3(0, _bounceSpeed * Time.deltaTime, 0);
    }
}
