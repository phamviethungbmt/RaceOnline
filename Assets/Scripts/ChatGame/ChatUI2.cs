using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChatUI2 : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatContent;
    public OpenCloseChatGame OpenCloseChatGame;
    private List<string> popUpMessages = new List<string>();
    public TextMeshProUGUI popupText;
    private void OnEnable()
    {
        sendButton.onClick.AddListener(SendMessage);
    }
    public void SendMessage()
    {
        string message = inputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            ChatManager2.Instance.SendChatMessage(message);
            inputField.text = "";
            if (!OpenCloseChatGame.panelChat.activeSelf)
            {
                OpenCloseChatGame.notificationPopup.SetActive(false);
            }
        }
    }
    public void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (OpenCloseChatGame.panelChat.activeSelf)
            {
                SendMessage();
            }
        }
    }
    public void NotifyNewMessage(string playerName, string message)
    {
        if (OpenCloseChatGame != null)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string formattedMessage = $"<color=black>[{time}]</color> <color=yellow>{playerName}</color>: <color=white>{message}</color>";
            popUpMessages.Add(formattedMessage);

            popupText.text += formattedMessage + "\n";
            if (!OpenCloseChatGame.panelChat.activeSelf)
            {
                OpenCloseChatGame.notificationPopup.SetActive(true);
            }
        }
    }
}
