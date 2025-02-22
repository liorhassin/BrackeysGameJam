using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 1f;
    private float timeLeft;
    public bool isFinished = false;
    private bool isStopped = true;

    void Start()
    {
        timeLeft = time; // Initialize timeLeft with the starting value
    }

    void Update()
    {
        if (isStopped)
        {
            return;
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f; // Ensure it doesn't go below 0
            isStopped = true;
            isFinished = true;
        }
    }

    public void StartTimer()
    {
        isStopped = false;
        isFinished = false;
        timeLeft = time; // Reset timeLeft to the original time when starting
    }

    public void StopTimer()
    {
        isStopped = true;
        isFinished = false;
    }

    public float TimeLeft()
    {
        return timeLeft;
    }
}
