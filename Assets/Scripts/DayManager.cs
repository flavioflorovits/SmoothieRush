using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }
    public int dayCount = 1;
    public float money;

    //For the day end stats
    private float moneyEarned = 0;
    private float moneySpent = 0;
    private int smoothiesMade = 0;
    private int smoothiesBotched = 0;
    private int ingredientsUsed = 0;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSmoothie()
    {
        smoothiesMade++;
    }

    public void AddIngredient()
    {
        ingredientsUsed++;
    }

    public void AddBotch()
    {
        smoothiesBotched++;
    }

    public void PaySmoothie(float amount)
    {
        money += amount;
        moneyEarned += amount;
    }

    public void PayIngredient(float amount)
    {
        AddIngredient();
        money -= amount;
        moneySpent += amount;
    }

    private void ResetStats()
    {
        smoothiesMade = 0;
        smoothiesBotched = 0;
        ingredientsUsed = 0;
        moneyEarned = 0;
        moneySpent = 0;
    }

    public void CompleteDay()
    {
        dayCount++;
        if (dayCount == 4)
        {
            WinGame();
            return;
        }
        SceneManager.LoadScene("Intermission");
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Intermission" || scene.name == "Win")
        {
            // Get a reference to the object containing the DisplayStats script
            StatDisplay scriptReference = FindObjectOfType<StatDisplay>();

            // Check if the script reference is not null
            if (scriptReference != null)
            {
                // Call the DisplayStats function
                StartCoroutine(scriptReference.DisplayStats(dayCount - 1, moneyEarned, moneySpent, money, smoothiesMade, smoothiesBotched, ingredientsUsed));
            }
        }
        else if(scene.name == "Main")
        {
            ResetStats();
            TimeHandler.Instance.RestartTime();
            StartCoroutine(CustomerManager.Instance.SpawnDelay());
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

}
