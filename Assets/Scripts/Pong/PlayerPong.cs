using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPong: MonoBehaviour
{
    PhotonView pw;

    TMPro.TextMeshProUGUI text;

    void Start()
    {
        text = GameObject.Find("Canvas/InfoText").GetComponent<TMPro.TextMeshProUGUI>();
        pw = GetComponent<PhotonView>();

        if(pw.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(0,-8,0);
                InvokeRepeating("PlayerControl",0,0.5f);
            }
            else
            {
                transform.position = new(0,8,0);
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
    void Move()
    {
        float x = Input.GetAxis("Mouse X") * 20;//Time.deltaTime * 20;
        transform.Translate(x,transform.position.y,0);
    }

    void PlayerControl()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            pw.RPC("ClearScreen", RpcTarget.All,null);
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("StartGame", RpcTarget.All, null);
            CancelInvoke("PlayerControl");
        }
    }

    [PunRPC]
    public void ClearScreen()
    {
        text.text = null;
    }
}