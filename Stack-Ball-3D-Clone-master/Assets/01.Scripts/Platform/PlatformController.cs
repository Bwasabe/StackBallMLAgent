using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformPartController[] _platforms = null;
    
    [SerializeField] private Vector2 _forceRandom = new Vector2(10f, 15f);
    [SerializeField] private Vector2 _torqueRandom = new Vector2(30, 270f);
    public void BreakAllPlatforms()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (PlatformPartController p in _platforms)
        {
            float force = Random.Range(_forceRandom.x, _forceRandom.y);
            float torque = Random.Range(_torqueRandom.x, _torqueRandom.y);
            
            p.BreakingPlatforms(force, torque);
        }
        
        Destroy(gameObject, 1f);
    }
}
