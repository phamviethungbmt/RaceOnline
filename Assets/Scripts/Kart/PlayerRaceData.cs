using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;




public class PlayerRaceData : NetworkBehaviour
{
    public static PlayerRaceData Instance;
    [Networked] public int LapCount { get; set; } = 1;
    [Networked] public float Distance { get; set; }
    [Networked] public NetworkString<_16> PlayerName { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    // Optional: callback để update UI nếu local player
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            RPC_SetPlayerName(ClientInfo.Username); // Gửi tên lên từ client
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetPlayerName(string name)
    {
        PlayerName = name;
    }
}
