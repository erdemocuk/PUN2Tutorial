using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour, IPoolable
{
    public float speed;
    public float posX_min,posX_max;
    public float posY_min,posY_max;

    public E_PoolObjectTypes type;
    
    public void SetRandomPos()
    {
        transform.position = new Vector3(posX_max,Random.Range(posY_min,posY_max), transform.position.z);
    }

    void FixedUpdate()
    {
        transform.Translate((-1)*speed*Time.fixedDeltaTime,0,0);
    }
}