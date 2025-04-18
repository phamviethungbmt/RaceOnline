using Fusion;
using System;
using System.Collections.Generic;
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
        string time = DateTime.Now.ToString("HH:mm:ss");
        string formattedMessage = $"<color=black>[{time}]</color> <color=yellow>{playerName}</color>: <color=white>{message}</color>"; chatMessage.Add(formattedMessage);
        chatMessage.Add(formattedMessage);
        ChatUI2.chatContent.text += formattedMessage + "\n"; 
    }
    public void SendChatMessage(string message)
    {
        string playerName = ClientInfo.Username.ToString();
        RpcReceiveChatMessage(playerName, message);
    }
}
