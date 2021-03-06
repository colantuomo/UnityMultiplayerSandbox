using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class GameConnection : MonoBehaviourPunCallbacks
{
    public InputField playerNameInput;
    public Text playersCount;
    public Text debugText;
    public Button joinBtn;
    public string roomForTesting = "default";
    private PhotonView _photonView;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _photonView = GetComponent<PhotonView>();
        playerNameInput.enabled = false;
        joinBtn.enabled = false;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        playerNameInput.enabled = true;
        joinBtn.enabled = true;
        UpdateRoomPlayers();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("A player entered the room " + PhotonNetwork.CurrentRoom);
        joinBtn.enabled = false;
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join room failed: " + returnCode + " - " + message);
    }

    public override void OnLeftRoom()
    {
        UpdateRoomPlayers();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomPlayers();
    }

    private void UpdateRoomPlayers()
    {
        playersCount.text = PhotonNetwork.CountOfPlayers + "";
    }

    public void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = playerNameInput.text;
        RoomOptions options = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomForTesting, options, TypedLobby.Default);
        playerNameInput.enabled = true;
        joinBtn.enabled = true;
        playerNameInput.text = "";
    }


}
