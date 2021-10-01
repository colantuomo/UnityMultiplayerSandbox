using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

using Photon.Pun;
using Photon.Realtime;

public class RoundManager : MonoBehaviourPunCallbacks
{
    public PhotonView _photonView;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("RPC_CheckPlayersOnline", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_CheckPlayersOnline()
    {
        Debug.Log("Players in that room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public void LeaveRoom()
    {
        LocalPlayer.CleanCustomProperties(PhotonNetwork.LocalPlayer);
        StartCoroutine(CleanData());
    }

    IEnumerator CleanData()
    {
        yield return new WaitForSeconds(.1f);
        PhotonNetwork.LeaveRoom();
    }
}
