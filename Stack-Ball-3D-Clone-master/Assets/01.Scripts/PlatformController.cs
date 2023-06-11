using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformPartController[] _platforms = null;

    public void BreakAllPlatforms()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<Player>().IncreaseScore();
        }

        foreach (PlatformPartController p in _platforms)
        {
            p.BreakingPlatforms();
        }
        
        Destroy(gameObject, 1f);
    }

}
