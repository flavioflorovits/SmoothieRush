using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmoothieRecipe", menuName = "Create Smoothie Recipe", order = 1)]

public class SmoothieSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string recipeName;

    public List<IngredientSO> ingredients = new List<IngredientSO>();
}
