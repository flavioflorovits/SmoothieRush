using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }
    public int dayCount;
    public float money;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteDay()
    {
        dayCount++;
        if (dayCount > 3)
        {
            WinGame();
            return;
        }
        TimeHandler.Instance.RestartTime();
        StartCoroutine(CustomerManager.Instance.SpawnDelay());

    }

    private void WinGame()
    {
        Debug.Log("You win.");
    }

}
