using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

using Photon.Pun;
using Photon.Realtime;
public class GameOverManager : MonoBehaviourPunCallbacks
{
    public Text scoreboard;
    private PhotonView _photonView;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        StartCoroutine(UpdateScoreboard());
    }

    IEnumerator UpdateScoreboard()
    {
        yield return new WaitForSeconds(.1f);
        scoreboard.text += Utils.UI.Scoreboard.AllPlayersScore(PhotonNetwork.CurrentRoom.Players);
    }

    public void RestartGame()
    {
        LocalPlayer.CleanCustomProperties(PhotonNetwork.LocalPlayer);
        _photonView.RPC("RPC_RestartGameScene", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_RestartGameScene()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
