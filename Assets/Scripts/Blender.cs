using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFilled = false;
    [SerializeField] private SpriteRenderer[] inputtedIngredients;
    public List<IngredientSO> inputtedSOs;
    [SerializeField] private SpriteRenderer combined;

    [SerializeField] private List<SmoothieSO> smoothieRecipes = new List<SmoothieSO>();

    [SerializeField] private SmoothieSO botchedSmoothieSO;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

                // Raycast to check if a blender is tapped
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("Blender"))
                {
                    Blend();
                } 
            }
        }
    }

    public void Blend()
    {
        if (isFilled)
        {

            Debug.Log("Blended");
            float r = 0f, g = 0f, b = 0f;
            int colorCount = 0;
            foreach (SpriteRenderer sprite in inputtedIngredients)
            {
                if (sprite.enabled)
                {
                    colorCount++;
                    r += sprite.color.r;
                    g += sprite.color.g;
                    b += sprite.color.b;
                }
            }
            SmoothieSO checkedRecipe = CheckSmoothie(inputtedSOs);
            Debug.Log(checkedRecipe);
            GameObject doneSmoothie;
            if (checkedRecipe != null)
            {
                doneSmoothie = checkedRecipe.prefab;
                GameObject newObject = Instantiate(doneSmoothie, transform.position, Quaternion.identity);
                Debug.Log(newObject.name);
                SmoothieInfo newObjectInfo = newObject.GetComponent<SmoothieInfo>();
                newObjectInfo.colorSprite.color = new Color(r/colorCount, g/colorCount, b/colorCount, 220f/255f);
                newObjectInfo.smoothieSO = checkedRecipe;
                newObjectInfo.blender = gameObject;
                newObject.name = checkedRecipe.recipeName;
            }
            foreach (SpriteRenderer ingredient in inputtedIngredients)
            {

                if (ingredient.enabled)
                {
                    ingredient.enabled = false;
                }

            }

            inputtedSOs.Clear();

            isFilled = false;

            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            inputtedSOs.Add(collision.gameObject.GetComponent<IngredientInfo>().ingredientSO);
            Debug.Log(inputtedIngredients[0]);
            foreach(SpriteRenderer ingredient in inputtedIngredients)
            {

                if (!ingredient.enabled)
                {
                    ingredient.color = collision.gameObject.GetComponent<IngredientInfo>().fruitColor;
                    ingredient.enabled = true;
                    Destroy(collision.gameObject);
                    isFilled = true;
                    break;
                }

            }
        }
    }

    private SmoothieSO CheckSmoothie(List<IngredientSO> playerBlend)
    {
        HashSet<IngredientSO> playerIngredients = new HashSet<IngredientSO>(playerBlend);

        foreach (var recipe in smoothieRecipes)
        {
            Debug.Log(playerBlend.Count);
            Debug.Log(recipe.ingredientCount);
            if (playerBlend.Count == recipe.ingredientCount)
            {
                HashSet<IngredientSO> recipeIngredients = new HashSet<IngredientSO>(recipe.ingredients);

                if (playerIngredients.SetEquals(recipeIngredients))     
                {
                    return recipe;
                }
            }
        }
        return botchedSmoothieSO;
    }

}