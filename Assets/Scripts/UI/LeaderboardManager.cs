using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System.Linq;
using UnityEngine;
using Fusion;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;



public class LeaderboardManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    const byte LeaderboardEventCode = 1;

    [SerializeField] private Transform leaderboardPanel; // cha chứa các dòng
    [SerializeField] private GameObject leaderboardEntryPrefab; // prefab hiển thị 1 người chơi

    private List<PlayerRaceData> players;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable() => PhotonNetwork.RemoveCallbackTarget(this);

    private void Awake()
    {
        players = new List<PlayerRaceData>();
    }

    private void Start()
    {
        SendDataToLeaderBoard(RoomPlayer.Players);
    }
    public void CalculateAndBroadcastLeaderboard()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        foreach (var p in PhotonNetwork.PlayerList)
        {
            int lap = p.CustomProperties.ContainsKey("Lap") ? (int)p.CustomProperties["Lap"] : 0;
            float dist = p.CustomProperties.ContainsKey("Dist") ? (float)(double)p.CustomProperties["Dist"] : 0f;

            players.Add(new PlayerRaceData(p.NickName, lap, dist));
        }

        var sorted = players.OrderByDescending(p => p.lapCount)
                            .ThenByDescending(p => p.distance)
                            .ToList();

        object[] data = sorted.Select(p => $"{p.playerName}|{p.lapCount}|{p.distance}").ToArray();

        PhotonNetwork.RaiseEvent(LeaderboardEventCode, data,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            new SendOptions { Reliability = false });
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == LeaderboardEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            List<PlayerRaceData> leaderboard = new List<PlayerRaceData>();

            foreach (string s in data)
            {
                var parts = s.Split('|');
                leaderboard.Add(new PlayerRaceData(parts[0], int.Parse(parts[1]), float.Parse(parts[2])));
            }

            DisplayLeaderboard(leaderboard);
        }
    }

    public void DisplayLeaderboard(List<PlayerRaceData> players)
    {
        foreach (Transform child in leaderboardPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count; i++)
        {
            var entry = Instantiate(leaderboardEntryPrefab, leaderboardPanel);
            entry.transform.Find("Rank").GetComponent<Text>().text = $"#{i + 1}";
            entry.transform.Find("Name").GetComponent<Text>().text = players[i].playerName;
            entry.transform.Find("Lap").GetComponent<Text>().text = $"{players[i].lapCount} / 3";
        }
    }
    public void SendDataToLeaderBoard(List<RoomPlayer> players)
    {
        this.players.Clear();
        foreach(var player in players)
        {
            string name = player.Username.ToString();
            PlayerRaceData newPlayerRace = new PlayerRaceData(name,1, 0);
            this.players.Add(newPlayerRace);
        }
        DisplayLeaderboard(this.players);
    }
    //public static void AddPlayerToLeaderBoardRoomPlayer(RoomPlayer player)
    //{
    //    List<PlayerRaceData> ListPlayers = new List<PlayerRaceData>();
    //    string playerName = player.Username.ToString();
    //    float _distance = 0;
    //    int _lapCount = 1;
    //    PlayerRaceData newPlayer = new PlayerRaceData(playerName, _lapCount, _distance);
    //    ListPlayers.Add(newPlayer);
    //    //Debug.Log("PlayerName = " + newPlayer.playerName);
    //    //Debug.Log("Lap = " + newPlayer.lapCount);
    //    //Debug.Log("Distance = " + newPlayer.distance);
    //    SendDataToLeaderBoard(ListPlayers);
    //}
}