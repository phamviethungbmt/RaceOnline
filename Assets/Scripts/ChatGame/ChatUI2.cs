using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI2 : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatContent;
    private void Start()
    {
        sendButton.onClick.AddListener(SendMessage);
    }
    private void SendMessage()
    {
        string message = inputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            ChatManager2.Instance.SendChatMessage(message);
            inputField.text = "";
        }
    }
}
