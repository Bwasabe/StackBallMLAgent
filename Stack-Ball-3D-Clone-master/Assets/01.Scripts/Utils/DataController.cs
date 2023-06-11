using System;
using System.Collections;
using System.Collections.Generic;

public class DataController<TBase> where TBase : class
{
    private Dictionary<Type, TBase> _dataDictionary = new();

    public T GetData<T>() where T : class, TBase
    {
        if(_dataDictionary.TryGetValue(typeof(T), out TBase value))
            return value as T;
        else
            throw new NullReferenceException($"{typeof(T)} is null in dictionary");
    }

    public void AddData(TBase data)
    {
        if(!_dataDictionary.TryAdd(data.GetType(), data))
            throw new Exception($"{data.GetType()} is already exist in Dictionary or null in dictionary");
    }
}
