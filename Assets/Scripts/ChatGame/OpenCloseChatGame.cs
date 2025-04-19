using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseChatGame : MonoBehaviour
{
    public GameObject panelChat;
    [SerializeField] private GameObject iconChat;
    public GameObject notificationPopup;

    public ChatUI2 chatUI;

    public void Update()
    {
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            bool isChatOpen = panelChat.activeSelf;

            panelChat.SetActive(!isChatOpen);
            iconChat.SetActive(isChatOpen);

            if (!isChatOpen)
            {
                notificationPopup.SetActive(false);
            }
            else
            {
                notificationPopup.SetActive(true);
            }
        }
    }
    public void OpenPanelChat()
    {
        panelChat.SetActive(true);
        iconChat.SetActive(false);
        notificationPopup.SetActive(false);
    }
    public void ClosePanelChat()
    {
        panelChat.SetActive(false);
        iconChat.SetActive(true);
        notificationPopup.SetActive(true);
    }
}
