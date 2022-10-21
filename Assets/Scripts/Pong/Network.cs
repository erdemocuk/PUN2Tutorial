using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject new_player = PhotonNetwork.Instantiate("PhotonPrefabs/PlayerPong", Vector3.zero, Quaternion.identity, 0);
            new_player.GetComponent<PhotonView>().Owner.NickName = "Something";
        }
    }
}