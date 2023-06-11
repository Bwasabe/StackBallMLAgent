using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformPartController : MonoBehaviour
{
    private Rigidbody _rb;
    private MeshRenderer _mr;
    private Collider _collider;
    [SerializeField] private float _moveSpeed = 1.5f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public void BreakingPlatforms()
    {
        _rb.isKinematic = false;
        _collider.enabled = false; 
        
        Vector3 parentPositionPoint = transform.parent.position;
        
        Vector3 direction = Vector3.up * _moveSpeed + (parentPositionPoint - _mr.bounds.center).normalized;
        direction.x *= -1f;
        direction.z *= -1f;
        
        direction.Normalize();

        _mr.material.DOFade(0f, 0.3f);
        
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        _rb.AddForceAtPosition(direction * force, parentPositionPoint, ForceMode.Impulse); 
        _rb.AddTorque(Vector3.left * torque);
        _rb.velocity = Vector3.down;
    }

    public void RemoveAllChildPlatforms()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
