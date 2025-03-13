using UnityEngine;
using TMPro; // Import TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime; // Countdown duration in seconds
    private float currentTime;
    public TextMeshProUGUI countdownText;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            TimerEnded();
        }
    }

    void UpdateTimerUI()
    {
        countdownText.text = "Time left: " + Mathf.Ceil(currentTime).ToString(); // Rounds up to avoid 0.1 flickering
    }

    void TimerEnded()
    {
        Debug.Log("Timer Finished!");
        countdownText.text = "TIME OUT";

        // Quit the game
        GameManager.Instance.QuitGame();
    }
}