using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPong: MonoBehaviour
{
    GameManagerPong gmPong;
    PhotonView pw;

    void Start()
    {
        gmPong = GameManagerPong.Instance;
        pw = GetComponent<PhotonView>();

        if(pw.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(0,-8,0);
            }
            else
            {
                Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
                cam.transform.Rotate(new Vector3(0, 0, 180));
                transform.position = new(0,8,0);
                gmPong.masterScoreText.rectTransform.sizeDelta = new Vector2(500,500);
                gmPong.clientScoreText.rectTransform.sizeDelta = new Vector2(500, -500);

            }
        }
    }

    void FixedUpdate()
    {
        if(pw.IsMine)
        {
            //Move();
        }
    }
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (pw.IsMine)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (pw.IsMine)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            if (curPosition.x > 2.5f)
            {
                curPosition.x = 2.5f;
            }
            else if (curPosition.x < -2.5f)
            {
                curPosition.x = -2.5f;
            }

            transform.position = curPosition;
        }
    }
}