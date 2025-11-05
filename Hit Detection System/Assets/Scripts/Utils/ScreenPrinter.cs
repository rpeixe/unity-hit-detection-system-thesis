using TMPro;
using UnityEngine;

public class ScreenPrinter : MonoBehaviour
{
    public static ScreenPrinter Instance { get; private set; }

    [SerializeField] private TMP_Text m_textPlayer1;
    [SerializeField] private TMP_Text m_textPlayer2;

    private void Awake()
    {
        Instance = this;
    }

    public void Print(string text, int playerID)
    {
        if (playerID == 1)
        {
            m_textPlayer1.text = text;
        }
        else
        {
            m_textPlayer2.text = text;
        }
    }
}
