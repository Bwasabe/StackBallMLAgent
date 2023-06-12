﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _camPosition;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _lastPlatform;
    private float _cameraDistance = 5f;

    private void Start()
    {
        if (_lastPlatform == null)
        {
            _lastPlatform = GameObject.Find("LastPlatform(Clone)").transform;
        }
    }
    
    void Update()
    {
        FollowTheBall();
    }

    private void FollowTheBall()
    {
        if (transform.position.y > _player.transform.position.y && transform.position.y > _lastPlatform.position.y + 4)
            _camPosition = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, _camPosition.y, -_cameraDistance);
    }
}
