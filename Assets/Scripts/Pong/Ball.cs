using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{

    Rigidbody rb;
    PhotonView pw;

    int P1_score = 0;
    int P2_score = 0;

    public TMPro.TextMeshProUGUI P1_score_txt;
    public TMPro.TextMeshProUGUI P2_score_txt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
    }

    [PunRPC]
    public void StartGame()
    {
        rb.velocity = new Vector3(5,5,0);
        ShowScore();
    }

    public void ShowScore()
    {
        P1_score_txt.text = PhotonNetwork.PlayerList[0].NickName + ": " + P1_score.ToString();
        P2_score_txt.text = PhotonNetwork.PlayerList[1].NickName + ": " + P2_score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(pw.IsMine)
        {
            if (collision.gameObject.CompareTag("P1_target"))
            {
                pw.RPC("GOAL", RpcTarget.All, 0 , 1);
            }
            else if (collision.gameObject.CompareTag("P2_target"))
            {
                pw.RPC("GOAL", RpcTarget.All, 1 , 0);
            }
        }
    }

    [PunRPC]
    public void GOAL(int P1, int P2)
    {
        P1_score += P1;
        P2_score += P2;

        ShowScore();

        NextRound();
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