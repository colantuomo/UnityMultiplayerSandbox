using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class CoinBehavior : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            EventsUI.current.CoinPickedUpTrigger(PhotonNetwork.LocalPlayer.NickName);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
