using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;
using Photon.Realtime;
public class ManagerUI : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        EventsUI.current.onCoinPickedUp += OnCoinPickedUp;
    }

    private void OnCoinPickedUp(string playerName)
    {
        Hashtable customProperties = new Hashtable();
        string key = "coin";
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(key))
        {
            customProperties.Add(key, 1);
        }
        else
        {
            string value = customProperties[key].ToString();
            customProperties[key] = value;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
        Debug.Log("Coin picked by the player: " + playerName);
    }

    private void OnDestroy()
    {
        EventsUI.current.onCoinPickedUp -= OnCoinPickedUp;
    }
}
