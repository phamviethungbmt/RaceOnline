using Photon.Pun;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    public bool debug;

    private void OnTriggerStay(Collider other) {
        if ( other.TryGetComponent(out KartLapController kart) ) {
            kart.ProcessFinishLine(this);
        }
    }
    public void OnLapPassed(int currentLap, float distance)
    {
        var props = new ExitGames.Client.Photon.Hashtable
        {
            { "Lap", currentLap },
            { "Dist", distance }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        if (PhotonNetwork.IsMasterClient)
        {
            FindObjectOfType<LeaderboardManager>().CalculateAndBroadcastLeaderboard();
        }
    }
}
