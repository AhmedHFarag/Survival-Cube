using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ObjectPool
{
    //the list of objects.
    private List<GameObject> pooledObjects;

    //private GameObject pooledObj;

    //initial and default number of objects to have in the list.
    private int initialPoolSize;

    //used if we need to grow the list.
    private GameObject pooledObj;

    //maximum number of objects to have in the list.
    private int maxPoolSize;

    public ObjectPool(GameObject obj, int initialPoolSize, int maxPoolSize)
    {
        //instantiate a new list of game objects to store our pooled objects in.
        pooledObjects = new List<GameObject>();

        //create and add an object based on initial size.
        for (int i = 0; i < initialPoolSize; i++)
        {
            //instantiate and create a game object with useless attributes.
            //these should be reset anyways.
            GameObject nObj = GameObject.Instantiate(obj);

            //make sure the object isn't active.
            nObj.SetActive(false);

            //add the object too our list.
            pooledObjects.Add(nObj);

            //Don't destroy on load, so
            //we can manage centrally.
            GameObject.DontDestroyOnLoad(nObj);
        }

        this.maxPoolSize = maxPoolSize;
        this.pooledObj = obj;
        this.initialPoolSize = initialPoolSize;
    }

    // returns Game Object of requested type if it is available, otherwise null.
    public GameObject GetObject()
    {
        //iterate through all pooled objects.
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //look for the first one that is inactive.
            if (pooledObjects[i].activeSelf == false)
            {
                //set the object to active.
                pooledObjects[i].SetActive(true);
                //return the object we found.
                return pooledObjects[i];
            }
        }
        //if we make it this far, we obviously didn't find an inactive object.
        //so we need to see if we can grow beyond our current count.
        if (this.maxPoolSize > this.pooledObjects.Count)
        {
            //Instantiate a new object.
            GameObject nObj = GameObject.Instantiate(pooledObj, Vector3.zero, Quaternion.identity) as GameObject;
            //set it to active since we are about to use it.
            nObj.SetActive(true);
            //add it to the pool of objects
            pooledObjects.Add(nObj);
            //return the object to the requestor.
            return nObj;
        }
        //if we made it this far obviously we didn't have any inactive objects
        //we also were unable to grow, so return null as we can't return an object.
        return null;
    }

    public void DisableObj(GameObject target)
    {
        target.SetActive(false);
    }

    //used to delete all the created Object in the stack, and clear stack
    public void Clear()
    {
        foreach (var item in pooledObjects)
        {
            GameObject.Destroy(item);
        }
        pooledObjects.Clear();
    }
    public bool CheckIfActive()
    {
        foreach (GameObject item in pooledObjects)
        {
            if(item.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
