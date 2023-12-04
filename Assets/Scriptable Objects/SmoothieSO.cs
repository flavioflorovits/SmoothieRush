using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmoothieSO", menuName = "Create Smoothie Recipe", order = 1)]

public class SmoothieSO : ScriptableObject
{
    public GameObject prefab;
    public int ingredientCount;
    public string recipeName;
    public float smoothiePrice;


    public List<IngredientSO> ingredients = new List<IngredientSO>();
}
