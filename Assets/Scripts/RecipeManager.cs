using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance{ get; private set; }

    public List<SmoothieSO> allSmoothies = new List<SmoothieSO>();

    public List<SmoothieSO> unlockedSmoothieRecipes = new List<SmoothieSO>();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
