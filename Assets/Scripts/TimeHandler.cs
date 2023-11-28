using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{

    public float roundTime;
    public bool timerRunning = false;
    [SerializeField] private TextMeshProUGUI timeText;
    public static TimeHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (roundTime > 0)
            {
                roundTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                roundTime = 0;
                timerRunning = false;
            }
            DisplayTime(roundTime);

        }
    }
    public void DisplayTime(float timeToDisplay)
    {
        if (timerRunning)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
        }
        else
        {
            timeText.text = "00:00";
        }
    }
}