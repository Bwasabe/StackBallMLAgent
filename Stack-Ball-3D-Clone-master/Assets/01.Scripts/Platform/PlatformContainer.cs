using System;
using UnityEngine;

[Serializable]
public class PlatformContainer
{
    [SerializeField] private PlatformController[] _platforms = new PlatformController[4];
    public PlatformController[] Platforms => _platforms;
}
