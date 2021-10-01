using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;
using Photon.Realtime;
using Utils.UI;
public class ManagerUI : MonoBehaviourPunCallbacks
{
    public Text TEXT_playersList;
    public Text TEXT_playerHealth;
    private PhotonView _photonView;
    private float coins = 0;
    private string COIN_KEY = "coin";
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        EventsUI.current.onCoinPickedUp += OnCoinPickedUp;
        EventsUI.current.onUpdatePlayerHealth += OnUpdatePlayerHealth;
    }

    private void OnCoinPickedUp(string userId)
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            Hashtable customProperties = new Hashtable();
            ++coins;
            customProperties.Add(COIN_KEY, coins);
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            StartCoroutine(OnUpdateScoreboard());
        }
    }

    private void OnDestroy()
    {
        EventsUI.current.onCoinPickedUp -= OnCoinPickedUp;
        // EventsUI.current.onUpdateScoreboard -= OnUpdateScoreboard;
    }

    IEnumerator OnUpdateScoreboard()
    {
        Debug.Log("OnUpdateScoreboard");
        yield return new WaitForSeconds(.1f);
        _photonView.RPC("RPC_UpdateRoomPlayers", RpcTarget.All);
    }

    void OnUpdatePlayerHealth(float health)
    {
        TEXT_playerHealth.text = health.ToString();
    }

    string ScoreboardPlayerLabel(KeyValuePair<int, Player> player)
    {
        string COIN_KEY = "coin";
        return player.Value.NickName + ": " + player.Value.CustomProperties[COIN_KEY];
    }

    [PunRPC]
    private void RPC_UpdateRoomPlayers()
    {
        TEXT_playersList.text = "";
        TEXT_playersList.text += Scoreboard.AllPlayersScore(PhotonNetwork.CurrentRoom.Players);
    }
}
