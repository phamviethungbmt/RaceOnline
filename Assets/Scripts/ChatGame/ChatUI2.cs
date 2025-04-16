using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChatUI2 : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatContent;
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
        }
    }
    public void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SendMessage();
        }
    }
}
