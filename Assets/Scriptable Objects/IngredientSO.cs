using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Create ingredient", order = 1)]

public class IngredientSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public float ingredientPrice;


    public string ingredientName;

}
