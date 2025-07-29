using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T _prefab;
    private Transform _container;

    private readonly List<T> _enabledPool = new List<T>();
    private readonly List<T> _disabledPool = new List<T>();

    public List<T> EnabledPool => _enabledPool;
    public List<T> DisabledPool => _disabledPool;

    public void InitializePool(T prefab, Transform container, int sizeOfPool = 5)
    {
        _container = container;
        _prefab = prefab;
        for (int i = 0; i < sizeOfPool; i++)
        {
            T @object = UnityEngine.Object.Instantiate(_prefab, _container, true);
            @object.gameObject.SetActive(false);
            _disabledPool.Add(@object);          
        }
    }
    public T GetFreeComponent(bool shitchOn = true)
    {
        T @object;
        if (_disabledPool.Count > 0)
        {
            @object = _disabledPool[0];       
            _disabledPool.RemoveAt(0);    
        }
        else
        {
            @object = UnityEngine.Object.Instantiate(_prefab, _container, true);
        }

        _enabledPool.Add(@object);

        if (shitchOn)
        {
            @object.gameObject.SetActive(true);
        }

        return @object;
    }

    public void DisableComponent(T @object)
    {
        
        @object.gameObject.SetActive(false);
        _disabledPool.Add(@object);
        _enabledPool.Remove(@object);
        @object.transform.SetParent(_container, false);
    }
}