using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseChatGame : MonoBehaviour
{
    [SerializeField] private GameObject panelChat;
    [SerializeField] private GameObject iconChat;
    public void OpenPanelChat()
    {
        panelChat.SetActive(true);
        iconChat.SetActive(false);
    }  
    public void ClosePanelChat()
    {
        panelChat.SetActive(false);
        iconChat.SetActive(true);
    }
}
