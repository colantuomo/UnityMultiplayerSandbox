using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class CoinBehavior : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            EventsUI.current.CoinPickedUpTrigger(PhotonNetwork.LocalPlayer.UserId);
            _photonView.RPC("RPC_Destroy", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Destroy()
    {
        Destroy(gameObject);
    }
}
