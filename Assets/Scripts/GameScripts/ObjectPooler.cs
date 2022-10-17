using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public EnemyObject baseObject;
    public int amount;
}

public class ObjectPooler
{
    public ObjectPoolItem item;
    public GameObject baseObject;
    bool shouldExpand = false;
    Queue<EnemyObject> pool;

    public void InitPool()
    {        
        pool = new Queue<EnemyObject>();
        for (int i = 0; i < item.amount; i++)
        {
            EnemyObject gameObject = Object.Instantiate(item.baseObject);
            gameObject.gameObject.SetActive(false);
            gameObject.transform.parent = baseObject.transform;
            pool.Enqueue(gameObject);
        }
    }

    public EnemyObject Get()
    {
        if (!shouldExpand)
        {
            EnemyObject gameObject = pool.Dequeue();
            if (pool.Count == 0)
            {
                shouldExpand = true;
            }
            return gameObject;
        }
        else
        {
            EnemyObject gameObject = Object.Instantiate(item.baseObject);
            gameObject.gameObject.SetActive(false);
            gameObject.transform.parent = baseObject.transform;
            item.amount++;
            return gameObject;
        }
    }

    public void Return(EnemyObject gameObject)
    {
        gameObject.transform.parent = baseObject.transform;
        pool.Enqueue(gameObject);
        shouldExpand = false;
    }
}

