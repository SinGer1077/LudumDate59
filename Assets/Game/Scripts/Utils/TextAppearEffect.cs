using TMPro;
using UnityEngine;

public class TextAppearEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float appearSpeed = 0.05f;

    private string initText;
    private int currentLetterIdx;

    private void Awake()
    {
        initText = text.text;
        text.text = "";
    }

    public void ActivateEffect()
    {
        text.text = "";
        currentLetterIdx = 0;
        AddLetter();
    }

    private void AddLetter()
    {
        if (currentLetterIdx < initText.Length)
        {
            text.text += initText[currentLetterIdx];
            currentLetterIdx++;
            Invoke("AddLetter", appearSpeed);
        }
    }
}
