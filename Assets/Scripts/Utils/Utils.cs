using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;
using Photon.Realtime;

namespace Utils
{
    public class LocalPlayer
    {
        public static void CleanCustomProperties(Player player)
        {
            Hashtable customProperties = new Hashtable();
            string COIN_KEY = "coin";
            customProperties.Add(COIN_KEY, 0);
            player.SetCustomProperties(customProperties);
        }
    }

    namespace UI
    {
        public class Scoreboard
        {
            private static string COIN_KEY = "coin";
            private static string ScoreboardPlayerLabel(KeyValuePair<int, Player> player, string coinValue)
            {
                return player.Value.NickName + ": " + coinValue;
            }
            public static string AllPlayersScore(Dictionary<int, Player> players)
            {
                string label = "";
                foreach (KeyValuePair<int, Player> item in players)
                {
                    if (item.Value.CustomProperties.ContainsKey(COIN_KEY))
                    {
                        label += ScoreboardPlayerLabel(item, item.Value.CustomProperties[COIN_KEY].ToString()) + "\n";
                    }
                    else
                    {
                        label += ScoreboardPlayerLabel(item, "0") + "\n";
                    }
                }
                return label;
            }
        }

    }
}
