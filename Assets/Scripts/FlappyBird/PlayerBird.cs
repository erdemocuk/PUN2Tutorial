using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerBird : MonoBehaviour
{
    Rigidbody2D rb; //Gravity 0.1
    GameManager gm;
    public float tap_power; //20
    public float angle_speed;
    public Action onGameOver;
    public Action onStart;
    bool isRunning = false;

    
    void Start()
    {
        gm = GameManager.instance;
        gm.button.onClick.AddListener(Button);
        onGameOver += gm.GameOver;
        onStart += gm.OnRaceStart;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGameOver?.Invoke();
    }
    void FixedUpdate()
    {
        if (isRunning)
            transform.eulerAngles -= new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle_speed); 
    }
    public void FirstTap()
    {
        rb.gravityScale = 1;
        Tap();
        isRunning = true;
        onStart?.Invoke();
    }
    public void Tap()
    {
        //rb.velocity = Vector2.zero;
        rb.velocity = tap_power * Vector2.up;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,60.0f);
    }
    public void Button()
    {
        if (!isRunning)
        {
            FirstTap();
        }
        else
        {
            Tap();
        }        
    }
}