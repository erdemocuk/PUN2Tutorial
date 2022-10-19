using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
    Rigidbody2D rb; //Gravity 0.1

    public float tap_power; //20
    public float angle_speed;
    public Action onGameOver;
    bool isRunning = false;

    void Start()
    {
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
    }
    public void Tap()
    {
        //rb.velocity = Vector2.zero;
        rb.velocity = tap_power * Vector2.up;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,60.0f);
    }
}