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
using Hashtable = ExitGames.Client.Photon.Hashtable;



public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] protected GameObject playersHolder;
    [Header("Options")]
    [SerializeField] protected float refreshRate = 1f;

    [SerializeField] protected GameObject[] slots;

    [SerializeField] protected Text[] nameTexts;
    [SerializeField] protected Text[] lapTexts;

    private void Start()
    {
        
    }
    public void UpdateLeaderboard()
    {
        // Tìm tất cả NetworkPlayer trong scene
        var allPlayers = FindObjectsOfType<PlayerRaceData>();

        var sortedPlayers = allPlayers
            .OrderByDescending(p => p.LapCount)
            .ThenByDescending(p => p.Distance)
            .ToList();

        // Ẩn tất cả trước
        foreach (var slot in slots)
            slot.SetActive(false);
        var listPlayerRaceInGame = GetStartKarts();
        for (int i = 0; i < sortedPlayers.Count && i < slots.Length; i++)
        {
            var slot = listPlayerRaceInGame[i];
            var p = sortedPlayers[i];
            slots[i].SetActive(true);
            nameTexts[i].text = string.IsNullOrEmpty(p.PlayerName.ToString()) ? slot.Controller.RoomUser.Username.Value : p.PlayerName.Value;
            lapTexts[i].text = $"<color=yellow>{p.LapCount.ToString()}</color>" + @"<color=black>/</color>" + "<color=#26FF00>3</color>";
            if (p.Object.HasInputAuthority)
            {
                nameTexts[i].color = Color.yellow;
            }
            else
            {
                nameTexts[i].color = Color.white;
            }
        }
    }
    private static List<KartEntity> GetStartKarts() =>
        KartEntity.Karts
            .ToList();
    private void Update()
    {
        UpdateLeaderboard();
    }
}