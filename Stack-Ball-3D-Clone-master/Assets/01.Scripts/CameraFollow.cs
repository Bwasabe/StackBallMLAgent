using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IWinAble
{
    [SerializeField] private Vector3 _camPosition;

    [SerializeField] private PlayerAgent _player;
    public Transform LastPlatform{ get; set; }
    private float _cameraDistance = 5f;

    private IEnumerator Start()
    {
        yield return null;
        _player.EpisodeBeginAction += WinGame;
    }

    void Update()
    {
        FollowTheBall();
    }

    private void FollowTheBall()
    {
        if (transform.position.y > _player.transform.position.y && transform.position.y > LastPlatform.position.y + 4)
            _camPosition = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, _camPosition.y, -_cameraDistance);
    }
    public void WinGame()
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        Destroy(LastPlatform.gameObject);
        yield return null;
        transform.position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        _camPosition = transform.position;

    }
}
