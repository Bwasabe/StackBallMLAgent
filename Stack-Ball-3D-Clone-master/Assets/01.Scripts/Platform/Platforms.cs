using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private PlatformContainer[] _platformContainers;

    private GameObject[] _selectedPlatforms = new GameObject[4];

    void Update()
    {
        transform.Rotate(new Vector3(0, _speed * Time.deltaTime, 0));
    }

    public GameObject GetSelectedPlatform(int rangeMin, int rangeMax)
    {
        return Instantiate(_selectedPlatforms[Random.Range(rangeMin, rangeMax)]);
    }
    
    
    public void PlatformSelection()
    {
        int randomModel = Random.Range(0, _platformContainers.Length);
        _selectedPlatforms = _platformContainers[randomModel].Platforms;
    }
}
