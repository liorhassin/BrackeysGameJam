using System;
using TMPro;
using UnityEngine;

public class DiffuseManager : TypingManager
{
    public TMP_InputField[] answerFields; // Assign in Inspector
    public LaptopInteractable laptopInteractable;

    private int[] answers = { 2, 56, 2, 20, 17 };
    private int currentIndex = 0;
    private bool isActive = false;
    public bool isSolved = false;

    void Start()
    {
        DisableTyping();
        isSolved = false;
    }

    void Awake()
    {
        DisableTyping();
        isSolved = false;
    }

    public override void EnableTyping()
    {
        isActive = true;
        if (answerFields.Length > 0 && currentIndex >= 0 && currentIndex < answerFields.Length)
        {
            answerFields[currentIndex].interactable = true;
            answerFields[currentIndex].onEndEdit.AddListener(CheckAnswer);
            answerFields[currentIndex].Select();
        }
    }

    public override void DisableTyping()
    {
        isActive = false;
        foreach (var field in answerFields)
        {
            field.interactable = false;
            field.onEndEdit.RemoveListener(CheckAnswer);
        }
    }

    void CheckAnswer(string input)
    {
        if (!isActive) return;

        if (int.TryParse(input, out int userAnswer) && userAnswer == answers[currentIndex])
        {
            answerFields[currentIndex].textComponent.color = Color.green;
            answerFields[currentIndex].interactable = false;
            answerFields[currentIndex].onEndEdit.RemoveListener(CheckAnswer);

            currentIndex++;
            if (currentIndex < answerFields.Length)
            {
                answerFields[currentIndex].interactable = true;
                answerFields[currentIndex].onEndEdit.AddListener(CheckAnswer);
                answerFields[currentIndex].Select();
            }
            else
            {
                laptopInteractable.ExitLaptopMode();
                Debug.Log("All answers correct!");
                isSolved = true;
            }
        }
        else
        {
            answerFields[currentIndex].interactable = false;
            answerFields[currentIndex].interactable = true;
            answerFields[currentIndex].Select();
        }
    }
}
