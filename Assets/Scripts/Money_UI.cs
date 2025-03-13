using UnityEngine;
using TMPro;

public class Money_UI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        // Initialize the UI with the current points
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + GameManager.Instance.points.ToString();
        }
        else
        {
            Debug.LogError("moneyText is not assigned in the Inspector!");
        }
    }
}