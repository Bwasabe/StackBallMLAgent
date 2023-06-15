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
        if(LastPlatform == null)return;
        if (transform.localPosition.y > _player.transform.localPosition.y && transform.localPosition.y > LastPlatform.localPosition.y + 4)
            _camPosition = new Vector3(transform.localPosition.x, _player.transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x, _camPosition.y, -_cameraDistance);
    }
    public void WinGame()
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        if(LastPlatform != null)
            Destroy(LastPlatform.gameObject);
        yield return null;
        transform.localPosition = new Vector3(transform.localPosition.x, _player.transform.localPosition.y, transform.localPosition.z);
        _camPosition = transform.localPosition;

    }
}
