using System.Collections.Generic;
using UnityEngine;

public enum E_PoolObjectTypes { Enemy }
public class PoolableObject : MonoBehaviour
{
    public E_PoolObjectTypes objectType;
} 
[System.Serializable]
public class ObjectPoolItem
{
    public PoolableObject objPrefab;
    public int amount;
}

public class ObjectPoolingSystem: MonoBehaviour
{
    public List<ObjectPoolItem> ObjectPoolItems;
    HashSet<ObjectPooler<PoolableObject>> objectPoolSet;

    private void Start()
    {
        objectPoolSet = new HashSet<ObjectPooler<PoolableObject>>();
        GameObject parentObject = new();
        parentObject.name = "Objects";
        parentObject.transform.parent = transform;
        foreach (ObjectPoolItem objectPoolItem in ObjectPoolItems)
        {
            objectPoolSet.Add(new ObjectPooler<PoolableObject>(objectPoolItem.objPrefab, objectPoolItem.amount, parentObject));
        }
    }

    public PoolableObject Get(E_PoolObjectTypes objectType)
    {
        foreach (ObjectPooler<PoolableObject> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                PoolableObject newObj = objectPool.Get();
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }
    public PoolableObject Get(E_PoolObjectTypes objectType, Vector3 pos)
    {
        foreach (ObjectPooler<PoolableObject> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                PoolableObject newObj = objectPool.Get();
                newObj.transform.position = pos;
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }
    public PoolableObject Get(E_PoolObjectTypes objectType, Vector3 pos, Quaternion q)
    {
        foreach (ObjectPooler<PoolableObject> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                PoolableObject newObj = objectPool.Get();
                newObj.transform.SetPositionAndRotation(pos, q);
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }

    public void Return(PoolableObject obj)
    {
        foreach (ObjectPooler<PoolableObject> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == obj.objectType)
            {
                // Reset all data to Prefab
                obj.gameObject.SetActive(false);
                obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
                objectPool.Return(obj);
            }
        }        
        //Error!
    }
}

public class ObjectPooler<T> where T : MonoBehaviour
{
    private readonly T objPrefab;
    private readonly int amount;
    private readonly GameObject parentObject;
    bool shouldExpand = false;
    Queue<T> pool;

    public ObjectPooler(T objPrefab, int amount, GameObject parentObject)
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
        obj.transform.parent = parentObject.transform;
        pool.Enqueue(obj);
        shouldExpand = false;
    }
}

