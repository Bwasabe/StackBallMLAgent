using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    private DataController<Component> _dataController = new();
    protected virtual void Awake()
    {
        Component[] components = GetComponentsInChildren<Component>();

        foreach (Component component in components)
        {
            _dataController.AddData(component);
        }
    }

    public new T GetComponent<T>() where T : Component
    {
        return _dataController.GetData<T>();
    }
}
