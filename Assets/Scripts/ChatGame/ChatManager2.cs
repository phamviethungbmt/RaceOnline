using Fusion;
//using Photon.Chat;
//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager2 : NetworkBehaviour
{
    public ChatUI2 ChatUI2;
    private List<string> chatMessage = new List<string>();
    public static ChatManager2 Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcReceiveChatMessage(string playerName, string message)
    {
        string formattedMessage = $"{playerName}: {message}";
        chatMessage.Add(formattedMessage);
        ChatUI2.chatContent.text += formattedMessage + "\n"; 
    }
    public void SendChatMessage(string message)
    {
        string playerName = Runner.LocalPlayer.PlayerId.ToString();
        RpcReceiveChatMessage(playerName, message);
    }
}
