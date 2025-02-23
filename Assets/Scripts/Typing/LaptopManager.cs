using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class LaptopManager : TypingManager
{
    public TMP_Text displayText;
    public TMP_Text progressText;
    public GameObject Pistol;
    public RawImage errorImage;
    public LaptopInteractable laptopInteractable;
    
    private string loadedText = "";
    private int currentIndex = 0;
    private int goalIndex = 0;
    private int charPerFrame = 160;
    private float lengthToAdd = 0f;
    private int typingSpeedMultiplier = 30;
    private int typingMaxCap = 300;
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
        if (!laptopInteractable.active)
        {
            errorImage.enabled = true;
            if (is_active){
                Debug.Log("is active was on");
                laptopInteractable.ExitLaptopMode();
            }
            return;
        }

        errorImage.enabled = false;

        if (!is_active){
            return;
        }

        // Accumulate lengthToAdd over time
        lengthToAdd += Time.deltaTime * charPerFrame;
        int lengthToAddInt = Mathf.FloorToInt(lengthToAdd); // Convert float to int

        if (lengthToAddInt > 0)
        {
            lengthToAdd -= lengthToAddInt; // Subtract used portion

            int charsToAdd = Mathf.Min(lengthToAddInt, goalIndex - currentIndex);
            if (charsToAdd > 0)
            {
                displayText.text += loadedText.Substring(currentIndex, charsToAdd);
                currentIndex += charsToAdd;
            }
        }

        // Limit the display text length by removing characters from the start
        int maxDisplayLength = 500; // Adjust this as needed
        if (displayText.text.Length > maxDisplayLength * 2)
        {
            displayText.text = displayText.text.Substring(displayText.text.Length - maxDisplayLength);
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
        TextAsset textAsset = Resources.Load<TextAsset>("cs_homework_20k");
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
