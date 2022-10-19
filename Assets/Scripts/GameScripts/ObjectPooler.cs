using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public BaseObject baseObject;
    public int amount;
}

public class ObjectPooler
{
    public ObjectPoolItem item;
    public GameObject baseObject;
    bool shouldExpand = false;
    Queue<BaseObject> pool;

    public void InitPool()
    {        
        pool = new Queue<BaseObject>();
        for (int i = 0; i < item.amount; i++)
        {
            BaseObject gameObject = Object.Instantiate(item.baseObject);
            gameObject.gameObject.SetActive(false);
            gameObject.transform.parent = baseObject.transform;
            pool.Enqueue(gameObject);
        }
    }

    public BaseObject Get()
    {
        if (!shouldExpand)
        {
            BaseObject gameObject = pool.Dequeue();
            if (pool.Count == 0)
            {
                shouldExpand = true;
            }
            return gameObject;
        }
        else
        {
            BaseObject gameObject = Object.Instantiate(item.baseObject);
            gameObject.gameObject.SetActive(false);
            gameObject.transform.parent = baseObject.transform;
            item.amount++;
            return gameObject;
        }
    }

    public void Return(BaseObject gameObject)
    {
        gameObject.transform.parent = baseObject.transform;
        pool.Enqueue(gameObject);
        shouldExpand = false;
    }
}

