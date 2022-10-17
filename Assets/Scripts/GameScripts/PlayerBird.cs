using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
    Rigidbody2D rb; //Gravity 0.1

    public float tap_power; //20
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,1.0f); 
    }
    public void FirstTap()
    {

    }
    public void Tap()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = Vector2.up * tap_power * Time.deltaTime;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,60.0f);
    }
}