using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    public Button button;
    public ObjectPoolingSystem pool;
    public float enemy_period;

    readonly List<EnemyObject> activeEnemies = new();
    readonly List<EnemyObject> deAvtiveEnemies = new();
    void Update()
    {
        foreach (EnemyObject enemy in activeEnemies)
        {
            if (enemy.transform.position.x <= enemy.posX_min)
            {
                deAvtiveEnemies.Add(enemy);
                break;
            }
        }
        foreach (EnemyObject enemy in deAvtiveEnemies)
        {
            activeEnemies.Remove(enemy);
            pool.Return(enemy);
        }
        deAvtiveEnemies.Clear();
    }
    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }    

    public void OnRaceStart()
    {
        InvokeRepeating(nameof(GetEnemy), 0.0f, enemy_period);
    }

    public void GetEnemy()
    {
        EnemyObject obj = pool.Get(E_PoolObjectTypes.Enemy) as EnemyObject;
        obj.SetRandomPos();
        activeEnemies.Add(obj);
    }
}