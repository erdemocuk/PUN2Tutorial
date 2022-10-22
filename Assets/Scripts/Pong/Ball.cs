using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    GameManagerPong gmPong;
    Rigidbody rb;
    PhotonView pw;

    void Start()
    {
        gmPong = GameManagerPong.Instance;
        rb = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();
    }

    //[PunRPC]
    public void StartGame()
    {
        rb.velocity = new Vector3(5,5,0);
        gmPong.ShowScore();
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if(pw.IsMine)
        {
            if (collision.gameObject.CompareTag("P1_target"))
            {
                gmPong.pw.RPC("GOAL", RpcTarget.All, PhotonNetwork.PlayerList[0]);
                //gmPong.GOAL(PhotonNetwork.PlayerList[0]);
                //pw.RPC("GOAL", RpcTarget.All, 0 , 1);
            }
            else if (collision.gameObject.CompareTag("P2_target"))
            {
                gmPong.pw.RPC("GOAL", RpcTarget.All, PhotonNetwork.PlayerList[1]);
                //gmPong.GOAL(PhotonNetwork.PlayerList[1]);
                //pw.RPC("GOAL", RpcTarget.All, 1 , 0);
            }
        }
    }   

    public void NextRound()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        //TODO:
        //After Some State Changes
        StartGame();
    }
}