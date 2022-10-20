using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PoolObjectTypes { }
public interface IPoolable
{
    E_PoolObjectTypes objectType;
} 
[System.Serializable]
public class ObjectPoolItem
{
    public IPoolable objPrefab;
    public int amount;
}

public class ObjectPoolManager : MonoBehaviour
{
    public List<ObjectPoolItem> ObjectPoolItems;
    HashSet<ObjectPooler<IPoolable>> objectPoolSet;

    private void Start()
    {
        objectPoolSet = new HashSet<ObjectPooler<IPoolable>>();
        GameObject parentObject = new();
        parentObject.name = "Objects";
        parentObject.transform.parent = transform;
        foreach (ObjectPoolItem objectPoolItem in ObjectPoolItems)
        {
            objectPoolSet.Add(new ObjectPooler<IPoolable>(objectPoolItem.objPrefab, objectPoolItem.amount, parentObject));
        }
    }

    public IPoolable Get(E_PoolObjectTypes objectType)
    {
        foreach (ObjectPooler<IPoolable> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                IPoolable newObj = objectPool.Get();
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }
    public IPoolable Get(E_PoolObjectTypes objectType, Vector3 pos)
    {
        foreach (ObjectPooler<IPoolable> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                IPoolable newObj = objectPool.Get();
                newObj.transform.position = pos;
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }
    public IPoolable Get(E_PoolObjectTypes objectType, Vector3 pos, Quaternion q)
    {
        foreach (ObjectPooler<IPoolable> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == objectType)
            {
                IPoolable newObj = objectPool.Get();
                newObj.transform.SetPositionAndRotation(pos, q);
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }
        return null; //Error!
    }

    public void Return(IPoolable obj)
    {
        foreach (ObjectPooler<IPoolable> objectPool in objectPoolSet)
        {
            if (objectPool.GetObjPrefab().objectType == gameObject.ObjectType)
            {
                // Reset all data to Prefab
                obj.gameObject.SetActive(false);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                objectPool.Return(gameObject);
            }
        }        
        //Error!
    }
}

