using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public List<ObjectPoolItem> ObjectPoolItems;
    HashSet<ObjectPooler> objectPoolSet;

    private void Start()
    {
        objectPoolSet = new HashSet<ObjectPooler>();
        GameObject gameObject = new();
        gameObject.name = "Objects";
        gameObject.transform.parent = this.transform;
        foreach (ObjectPoolItem objectPoolItem in ObjectPoolItems)
        {
            ObjectPooler newPool = new()
            {
                item = objectPoolItem,
                baseObject = gameObject
            };
            newPool.InitPool();
            objectPoolSet.Add(newPool);
        }
    }

    public BaseObject Get(BaseObject.E_ObjectType objectType)
    {
        foreach (ObjectPooler objectPool in objectPoolSet)
        {
            if (objectPool.item.baseObject.ObjectType == objectType)
            {
                BaseObject new_object = objectPool.Get();
                new_object.gameObject.SetActive(true);
                return new_object;
            }
        }
        return null; //Error!
    }
    public BaseObject Get(BaseObject.E_ObjectType objectType, Vector3 pos)
    {
        foreach (ObjectPooler objectPool in objectPoolSet)
        {
            if (objectPool.item.baseObject.ObjectType == objectType)
            {
                BaseObject new_object = objectPool.Get();
                new_object.transform.position = pos;
                new_object.gameObject.SetActive(true);
                return new_object;
            }
        }
        return null; //Error!
    }
    public BaseObject Get(BaseObject.E_ObjectType objectType, Vector3 pos, Quaternion q)
    {
        foreach (ObjectPooler objectPool in objectPoolSet)
        {
            if (objectPool.item.baseObject.ObjectType == objectType)
            {
                BaseObject new_object = objectPool.Get();
                new_object.transform.SetPositionAndRotation(pos, q);
                new_object.gameObject.SetActive(true);
                return new_object;
            }
        }
        return null; //Error!
    }

    public void Return(BaseObject gameObject)
    {
        foreach (ObjectPooler objectPool in objectPoolSet)
        {
            if (objectPool.item.baseObject.ObjectType == gameObject.ObjectType)
            {
                // Reset all data to Prefab
                gameObject.gameObject.SetActive(false);
                gameObject.transform.position = this.transform.position;
                gameObject.transform.rotation = this.transform.rotation;
                objectPool.Return(gameObject);
            }
        }        
        //Error!
    }
}

