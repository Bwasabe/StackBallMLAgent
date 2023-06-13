using System;
using UnityEngine;

[Serializable]
public class PlatformContainer
{
    [SerializeField] private GameObject[] _platforms = new GameObject[4];
    public GameObject[] Platforms => _platforms;
}
