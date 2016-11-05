using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PoolManager
{   
    private Dictionary<String, ObjectPool> objectPools;

    public PoolManager()
    {
        //Ensure object pools exists.
        this.objectPools = new Dictionary<String, ObjectPool>();
    }

    public ObjectPool CreatePool(GameObject objToPool, int initialPoolSize, int maxPoolSize)
    {
        //Check to see if the pool already exists.
        if (objectPools.ContainsKey(objToPool.name))
        {
            ObjectPool nPool;
            objectPools.TryGetValue(objToPool.name,out nPool);
            //objectPools.Remove(objToPool.name);
            //nPool.Clear();

            //nPool = new ObjectPool(objToPool, initialPoolSize, maxPoolSize);

            //objectPools.Add(objToPool.name, nPool);
            return nPool;
        }
        else
        {
            //create a new pool using the properties
            ObjectPool nPool = new ObjectPool(objToPool, initialPoolSize, maxPoolSize);
            //Add the pool to the dictionary of pools to manage
            //using the object name as the key and the pool as the value.
            
            objectPools.Add(objToPool.name, nPool);
            //We created a new pool!
            return nPool;
        }
    }

    public GameObject GetObject(string objName)
    {
        //Find the right pool and ask it for an object.
        //PoolManager.Instance.
        return objectPools[objName].GetObject();
    }
}



