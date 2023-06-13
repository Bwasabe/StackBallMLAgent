using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platforms : MonoBehaviour, IWinAble
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private PlayerAgent _playerAgent;
    [SerializeField] private MeshRenderer _cylinderMesh;

    [SerializeField] private PlatformContainer[] _platformContainers;
    
    private PlatformController[] _selectedPlatforms = new PlatformController[4];
    private Material _playerMat;
    private Material _platformMat;
    private Material _cylinderMat;
    
    private Color _platformColor;

    private List<PlatformController> _platformList = new();

    private void Start()
    {
        _playerAgent.EpisodeBeginAction += OnEpisodeBegin;
        _playerMat = _playerAgent.ComponentController.GetComponent<MeshRenderer>().material;
        _cylinderMat = _cylinderMesh.material;
    }
    
    private void OnEpisodeBegin()
    {
        WinGame();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, _speed * Time.deltaTime, 0));
    }

    public GameObject GetSelectedPlatform(int rangeMin, int rangeMax)
    {
        PlatformController platformController = Instantiate(_selectedPlatforms[Random.Range(rangeMin, rangeMax)]);
        _platformList.Add(platformController);
        
        return platformController.gameObject;
    }

    private void RemoveAllPlatforms()
    {
        foreach (PlatformController platformController in _platformList)
        {
            if(platformController == null) continue;
            
            if(platformController.gameObject.activeInHierarchy)
                Destroy(platformController.gameObject);
        }
        _platformList.Clear();
    }
    
    
    public void PlatformSelection()
    {
        int randomModel = Random.Range(0, _platformContainers.Length);
        _selectedPlatforms = _platformContainers[randomModel].Platforms;
    }
    public void WinGame()
    {
        _platformColor = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
        _cylinderMat.color = _platformColor + Color.gray;
        
        _playerMat.color = _platformColor;
        
        RemoveAllPlatforms();
    }
}
