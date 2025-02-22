using UnityEngine;
using TMPro;

public class LaptopManager : TypingManager
{
    public TMP_Text displayText;
    public TMP_Text progressText;
    public GameObject Pistol;
    
    private string loadedText = "";
    private int currentIndex = 0;
    private int goalIndex = 0;
    private int charPerFrame = 10;
    private int typingSpeedMultiplier = 50;
    private int typingMaxCap = 50;
    private bool is_active = false;
    private bool pistol_was_active = false;
    private float progress = 0;

    void Start()
    {
        LoadTextFile();
        displayText.text = "";
    }

    void Update()
    {   
        if (!is_active){
            return;
        }

        for (int i = 0 ; i < charPerFrame && currentIndex < goalIndex; i++) {
            displayText.text += loadedText[currentIndex];
            currentIndex++;
        }
        progress = (float)currentIndex / loadedText.Length * 100;
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

    public override void EnableTyping()
    {
        is_active = true;
        pistol_was_active = Pistol.activeSelf;
        Pistol.SetActive(false);
    }

    public override void DisableTyping()
    {
        is_active = false;
        goalIndex = currentIndex;
        Pistol.SetActive(pistol_was_active);
    }

    public float GetProgress() {
        return progress;
    }
}
