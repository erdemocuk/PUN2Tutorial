using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManagerPong : SingletonPUN<GameManagerPong>
{
    int masterScore = 0;
    int clientScore = 0;

    public Ball ball;
    public PhotonView pw;

    public TMPro.TextMeshProUGUI masterScoreText;
    public TMPro.TextMeshProUGUI clientScoreText;
    public TMPro.TextMeshProUGUI infoText;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
        StartCoroutine(StartGameRoutine());     
    }
    IEnumerator StartGameRoutine()
    {
        infoText.text = "Oyun Başlıyor !!                3";
        yield return new WaitForSeconds(1f);
        infoText.text = "Oyun Başlıyor !!                2";
        yield return new WaitForSeconds(1f);
        infoText.text = "Oyun Başlıyor !!                1";
        yield return new WaitForSeconds(1f);
        infoText.text = null;
        ball.StartGame();
    }
    public void ShowScore()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
            {
                masterScoreText.text = player.NickName + ": " + masterScore.ToString();
            }
            else
            {
                clientScoreText.text = player.NickName + ": " + clientScore.ToString();
            }
        }        
    }

    [PunRPC]
    public void GOAL(Player player)
    {
        if(player.IsMasterClient)
        {
            masterScore++;
        }
        else
        {
            clientScore++;
        }

        ShowScore();

        ball.NextRound();
    }
}
