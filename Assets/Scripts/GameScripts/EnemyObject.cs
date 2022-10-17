using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public enum E_ObjectType { Enemy }
    public E_ObjectType ObjectType;
    public float speed;
    public float posX_min,posX_max;
    public float posY_min,posY_max;

    void Start()
    {
        transform.position = new Vector3(posX_max,Random.Range(posY_min,posY_max), transform.position.z);
    }

    void Update()
    {
        transform.Translate((-1)*speed*Time.deltaTime,0,0);
    }
}