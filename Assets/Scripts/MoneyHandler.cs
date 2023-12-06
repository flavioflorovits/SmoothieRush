using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeText;

    public static MoneyHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

    }

    public void UpdateMoney(float money)
    {
        timeText.text = "€ " + money.ToString("N1");
    }

}
