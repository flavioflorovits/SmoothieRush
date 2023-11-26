using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothieInfo : MonoBehaviour
{

    public SpriteRenderer colorSprite;
    public SmoothieSO smoothieSO;
    public GameObject blender;
    public Color smoothieColor;
    public GameObject orderCanvas;
    
    public Image[] orderIngredients;

    private void OnEnable()
    {
        Debug.Log("Enabled smoothie");
        float r = 0, g = 0, b = 0;
        int colorCount = 0;
        if (smoothieSO != null) 
        {
            foreach (IngredientSO ingredient in smoothieSO.ingredients)
            {
                orderIngredients[colorCount].sprite = ingredient.sprite;
                IngredientInfo ingredientInfo = ingredient.prefab.GetComponent<IngredientInfo>();
                colorCount++;
                r = ingredientInfo.fruitColor.r;
                g = ingredientInfo.fruitColor.g;
                b = ingredientInfo.fruitColor.b;

            }

            smoothieColor = new Color(r/colorCount, g/colorCount, b/colorCount, 220f / 255f);
            colorSprite.color = smoothieColor;
        }

    }

    private void OnDisable()
    {
        Debug.Log("Disabled smoothie");
        if (smoothieSO != null)
        {
            for (int i = 0; i < orderIngredients.Length; i++)
            {
                orderIngredients[i].sprite = null;
                orderIngredients[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        if(blender != null)
        { 
            blender.SetActive(true);
        }
    }

}
