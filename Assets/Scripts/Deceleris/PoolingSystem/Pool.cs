using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{

    [Header ("SETTINGS")]
    public PoolObject prefab;
    public int initNumber = 10;

    [Header("DEBUG")]
    public List<PoolObject> inactives = new List<PoolObject>();
    public List<PoolObject> instances = new List<PoolObject>();

    public void Awake()
    {
        for (int i = 0; i < initNumber; i++) CreateNewObject();
    }

    public PoolObject SortObject ()
    {
        if (inactives.Count == 0) 
            CreateNewObject();
        return inactives[0];
    }

    public PoolObject CreateNewObject ()
    {
        PoolObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(transform);
        newObject.OnCreate(this);

        return newObject;
    }
}