using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingManager : MonoBehaviour
{
    public TMP_Text displayText;
    public TMP_Text progressText;

    private string loadedText = "";
    private int currentIndex = 0;
    private int goalIndex = 0;
    private int charPerFrame = 5;
    private int typingSpeedMultiplier = 10;
    private int typingMaxCap = 10;

    void Start()
    {
        LoadTextFile();
        displayText.text = "";
    }

    void Update()
    {   
        for (int i = 0 ; i < charPerFrame && currentIndex < goalIndex; i++) {
            displayText.text += loadedText[currentIndex];
            currentIndex++;
        }
        float progress = (float)currentIndex / loadedText.Length * 100;
        progressText.text = progress.ToString("F4") + "%";

        if (goalIndex < loadedText.Length && Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsLetter(c) || char.IsDigit(c) || c == ' ')
                {
                    goalIndex += typingSpeedMultiplier;

                    if (goalIndex > currentIndex + typingMaxCap)
                    {
                        goalIndex = currentIndex + typingMaxCap;
                    }
                }
            }
        }
    }

    void LoadTextFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("J. K. Rowling - Harry Potter 1 - Sorcerer's Stone");
        if (textAsset != null)
        {
            loadedText = textAsset.text;
        }
        else
        {
            Debug.LogError("Text file not found!");
        }
    }
}
