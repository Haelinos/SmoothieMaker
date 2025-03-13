using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Money_UI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void UpdateMoneyUI() 
    {
        moneyText.text = "Money: " + GameManager.Instance.points.ToString();

    }
}
