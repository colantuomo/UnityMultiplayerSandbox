using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsUI : MonoBehaviour
{
    public static EventsUI current;
    private void Awake()
    {
        current = this;
    }

    public event Action<string> onCoinPickedUp;
    public void CoinPickedUpTrigger(string playerName)
    {
        if (onCoinPickedUp != null)
        {
            onCoinPickedUp(playerName);
        }
    }

    public event Action onUpdateScoreboard;
    public void UpdateScoreboardTrigger()
    {
        if (onUpdateScoreboard != null)
        {
            onUpdateScoreboard();
        }
    }

    public event Action<float> onUpdatePlayerHealth;
    public void UpdatePlayerHealthTrigger(float health)
    {
        if (onUpdatePlayerHealth != null)
        {
            onUpdatePlayerHealth(health);
        }
    }

    public event Action<string> onPlayersDeath;
    public void OnPlayersDeathTrigger(string nickname)
    {
        if (onPlayersDeath != null)
        {
            onPlayersDeath(nickname);
        }
    }
}
