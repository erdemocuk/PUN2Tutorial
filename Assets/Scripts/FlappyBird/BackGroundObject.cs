using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObject : MonoBehaviour
{
    public float speed;
    public float posX_min,posX_max;
    public float posY_min,posY_max;

    void FixedUpdate()
    {
        if(transform.position.x <= posX_min)
        {
            transform.position = new Vector3(posX_max,Random.Range(posY_min,posY_max), transform.position.z);
        }
        transform.Translate((-1)*speed*Time.fixedDeltaTime,0,0);
    }
}