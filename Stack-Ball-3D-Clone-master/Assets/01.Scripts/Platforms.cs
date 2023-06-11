using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float speed = 100f;
    void Update()
    {
        // TODO: 플레이어의 키 입력으로 좌 우가 결정되도록
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
