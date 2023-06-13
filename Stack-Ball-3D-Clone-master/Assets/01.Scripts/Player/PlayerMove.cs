using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponentBase
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = PlayerAgent.ComponentController.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        if(PlayerAgent.IsDown)
        {
            _rb.velocity = new Vector3(0, -_moveSpeed, 0);
        }
    }
}
