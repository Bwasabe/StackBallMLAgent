using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _winPrefab;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private Platforms _platforms;
    
    private GameObject _normalPlatforms, _winPlatform;
    public int Level{ get; set; } = 1; 
    private int _platformAddition = 7;
    public Material plateMaterial, baseMaterial;
    public MeshRenderer playerMesh;

    void Awake()
    {
        LevelManagement();
    }

    private void LevelManagement()
    {
        plateMaterial.color = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
        baseMaterial.color = plateMaterial.color + Color.gray;
        playerMesh.material.color = plateMaterial.color;
        // currentLevelImage.color = plateMaterial.color;
        // nextLevelImage.color = plateMaterial.color;
        // progressBarImage.color = plateMaterial.color;

        Level = PlayerPrefs.GetInt("Level", 1);
        if (Level > 9)
            _platformAddition = 0;

        _platforms.PlatformSelection();
        
        float i = 0;
        for (; i > -Level - _platformAddition; i -= 0.5f)
        {
            if (Level <= 40)
                _normalPlatforms = _platforms.GetSelectedPlatform(0,2);
            if (Level > 40 && Level <= 80)
                _normalPlatforms = _platforms.GetSelectedPlatform(1,3);
            if (Level > 80 && Level <= 140)
                _normalPlatforms = _platforms.GetSelectedPlatform(2,4);
            if (Level > 140)
                _normalPlatforms = _platforms.GetSelectedPlatform(3,4);

            _normalPlatforms.transform.position = new Vector3(0, i, 0);
            _normalPlatforms.transform.eulerAngles = new Vector3(0, i * _rotationSpeed, 0);

            if (Mathf.Abs(i) >= Level * .3f && Mathf.Abs(i) <= Level * .6f) 
            {
                _normalPlatforms.transform.eulerAngles += Vector3.up * 180;
            }

            _normalPlatforms.transform.parent = _platforms.transform;
        }

        _winPlatform = Instantiate(_winPrefab);
        _winPlatform.transform.position = new Vector3(0, i, 0);
    }
}
