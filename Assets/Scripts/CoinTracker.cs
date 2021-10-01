using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class CoinTracker : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        Debug.Log("Coins: " + transform.childCount);
        if (transform.childCount <= 0)
        {
            _photonView.RPC("RPC_GoToGameOver", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_GoToGameOver()
    {
        SceneManager.LoadScene("Gameover");
    }
}

