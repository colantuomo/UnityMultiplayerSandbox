using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
public class PlayerHealth : MonoBehaviourPunCallbacks
{
    public float health = 10f;
    private GameObject gameOverPanel;
    public PhotonView _photonView;

    void Start()
    {
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }
        _photonView.GetComponent<PhotonView>();
        EventsUI.current.UpdatePlayerHealthTrigger(health);
    }

    void Update()
    {
        bool isDead = health <= 0;
        if (isDead)
        {
            EventsUI.current.UpdatePlayerHealthTrigger(health);
            _photonView.RPC("RPC_Death", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Death()
    {
        gameOverPanel.SetActive(true);
        EventsUI.current.OnPlayersDeathTrigger(PhotonNetwork.LocalPlayer.NickName);
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (_photonView.IsMine)
        {
            EventsUI.current.UpdatePlayerHealthTrigger(health);
        }
    }
}
