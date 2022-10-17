using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerBird player;
    public ObjectPoolManager pool;
    public float enemy_period;
    HashSet<EnemyObject> active_Enemies = new();
    bool start = false;


    // State Machine Kurulabilir !
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!start)
            {
                InvokeRepeating("GetEnemy",0.0f,enemy_period);
                //player.FirstTap();
                player.Tap();
                start = true;
            }
            else
            {
                player.Tap();
            }
        }
        foreach(EnemyObject obj in active_Enemies)
        {
            if(obj.transform.position.x <= obj.posX_min)
            {
                pool.Return(obj);
            }
        }
    }

    void GetEnemy()
    {
        active_Enemies.Add(pool.Get(EnemyObject.E_ObjectType.Enemy,Vector3.zero,Quaternion.identity));
    }
}