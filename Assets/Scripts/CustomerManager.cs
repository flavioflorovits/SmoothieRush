using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    [SerializeField] private List<GameObject> customers;
    [SerializeField] private List<Sprite> customerModels;
    private List<SmoothieSO> availableRecipes = new List<SmoothieSO>();
    [SerializeField] private int intervalMin, intervalMax;


    private void Start()
    {
        availableRecipes = RecipeManager.Instance.unlockedSmoothieRecipes;
        StartCoroutine(SpawnDelay());    
    }


    public IEnumerator SpawnDelay()
    {
        bool serviceComplete = false;
        do
        {
            int randomNumber = Random.Range(0, customers.Count);

            if (!customers[randomNumber].transform.Find("Order").gameObject.activeInHierarchy)
            {
                SmoothieSO selectedRecipe = availableRecipes[Random.Range(0, availableRecipes.Count)];
                Customer customer = customers[randomNumber].GetComponent<Customer>();
                customers[randomNumber].GetComponent<Image>().sprite = customerModels[Random.Range(0, customerModels.Count)];
                customer.smoothieSObject = selectedRecipe;
                customer.smoothieInfo.smoothieSO = selectedRecipe;
                customers[randomNumber].GetComponent<Image>().enabled = true;
                customers[randomNumber].transform.Find("Order").gameObject.SetActive(true);
                customers[randomNumber].SetActive(true);
                customer.EnableIngredients();
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        } while (TimeHandler.Instance.timerRunning);

        while (!serviceComplete)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                serviceComplete = true;
                if (customers[i].transform.Find("Order").gameObject.activeInHierarchy)
                {
                    serviceComplete = false;
                    break;
                }
            }
            yield return new WaitForSeconds(2f);
        }

        DayManager.Instance.CompleteDay();
    }

}
