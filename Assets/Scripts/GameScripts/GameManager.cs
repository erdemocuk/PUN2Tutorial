using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerBird player;
    public ObjectPoolManager pool;
    public float enemy_period;
    List<EnemyObject> activeEnemies = new();
    List<EnemyObject> deAvtiveEnemies = new();
    bool start = false;


    // State Machine Kurulabilir !
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player.onGameOver += GameOver;
    }
    private void GameOver()
    {
        SceneManager.LoadScene(1);
    }
    public void Button()
    {
        if(!start)
        {
            InvokeRepeating("GetEnemy",0.0f, enemy_period);
            player.FirstTap();
            start = true;
        }
        else
        {
            player.Tap();
        }
        foreach (EnemyObject enemy in activeEnemies)
        {
            if (enemy.transform.position.x <= enemy.posX_min)
            {
                deAvtiveEnemies.Add(enemy);
                break;
            }
        }
        foreach(EnemyObject enemy in deAvtiveEnemies)
        {
            activeEnemies.Remove(enemy);
            pool.Return(enemy);
        }
        deAvtiveEnemies.Clear();
    }

    void GetEnemy()
    {
        EnemyObject obj = pool.Get(BaseObject.E_ObjectType.Enemy) as EnemyObject;
        obj.SetRandomPos();
        activeEnemies.Add(obj);
    }
}