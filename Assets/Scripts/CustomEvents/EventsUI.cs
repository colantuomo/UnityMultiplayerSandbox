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
}
