using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    public Transform spawn;
    public Text TEXT_playersList;
    private PhotonView _photonView;
    void Start()
    {
        SetupInitialConfigs();
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("RPC_UpdateRoomPlayers", RpcTarget.All);

    }

    private void SetupInitialConfigs()
    {
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        Color color = Random.ColorHSV();
        var randomColor = new Vector3(color.r, color.g, color.b);
        object[] myCustomInitData = { randomColor };
        PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0, myCustomInitData);
    }

    [PunRPC]
    private void RPC_UpdateRoomPlayers()
    {
        TEXT_playersList.text = "";
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            TEXT_playersList.text += item.Value.NickName + "\n";
        }
    }
}
