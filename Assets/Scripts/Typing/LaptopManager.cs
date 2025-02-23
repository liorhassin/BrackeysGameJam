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
    private int charPerFrame = 100;
    private int typingSpeedMultiplier = 800;
    private int typingMaxCap = 10000;
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
            else{
                Debug.Log("is active was off");
            }
            return;
        }

        errorImage.enabled = false;
        
        if (!is_active){
            return;
        }

        // Calculate the next chunk of text to display
        int lengthToAdd = Mathf.Min(charPerFrame, goalIndex - currentIndex);
        if (lengthToAdd > 0)
        {
            // Add the substring from the loaded text
            displayText.text += loadedText.Substring(currentIndex, lengthToAdd);
            currentIndex += lengthToAdd;
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
