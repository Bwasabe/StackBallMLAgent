using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAgent))]
public class PlayerComponentBase : MonoBehaviour
{
    protected PlayerAgent PlayerAgent;

    protected virtual void Awake()
    {
        PlayerAgent = GetComponent<PlayerAgent>();
    }
}
