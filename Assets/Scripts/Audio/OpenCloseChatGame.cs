using Photon.Chat.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseChatGame : MonoBehaviour
{
    [SerializeField] private GameObject panelChat;
    [SerializeField] private GameObject iconChat;
    public ChatUI2 chatUI;  
    public void Update()
    {
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            if (!panelChat.activeSelf)
            {
                panelChat.SetActive(true);
                iconChat.SetActive(false);
            }
            else
            {
                panelChat.SetActive(false);
                iconChat.SetActive(true);
            }
        }
    }
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
