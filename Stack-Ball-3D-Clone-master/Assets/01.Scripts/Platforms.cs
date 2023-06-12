using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private float _speed = 180f;
    
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, _speed * Time.deltaTime, 0));
            
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, -_speed * Time.deltaTime, 0));
            
        }
    }
}
