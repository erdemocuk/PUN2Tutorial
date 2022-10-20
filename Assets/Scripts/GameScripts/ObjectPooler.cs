using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> where T: MonoBehaviour
{
    private T objPrefab;
    private int amount;
    private GameObject parentObject;
    bool shouldExpand = false;
    Queue<T> pool;

    ObjectPooler<T>(T objPrefab, int amount, GameObject parentObject)
    {
        this.objPrefab = objPrefab;
        this.amount = amount;
        this.parentObject = parentObject;
        InitPool();
    }
    private void InitPool()
    {        
        pool = new Queue<T>();
        for (int i = 0; i < amount; i++)
        {
            T newObj = Object.Instantiate(objPrefab);
            newObj.gameObject.SetActive(false);
            newObj.transform.parent = parentObject.transform;
            pool.Enqueue(newObj);
        }
    }
    public T GetObjPrefab()
    {
        return objPrefab;
    }
    public T Get()
    {
        if (!shouldExpand)
        {
            T obj = pool.Dequeue();
            if (pool.Count == 0)
            {
                shouldExpand = true;
            }
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(objPrefab);
            newObj.gameObject.SetActive(false);
            newObj.transform.parent = parentObject.transform;
            return newObj;
        }
    }

    public void Return(T obj)
    {
        gameObject.transform.parent = parentObject.transform;
        pool.Enqueue(obj);
        shouldExpand = false;
    }
}

