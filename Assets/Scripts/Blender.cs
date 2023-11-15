using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFilled = false;
    [SerializeField] private SpriteRenderer[] inputtedIngredients;
    [SerializeField] private List<IngredientSO> inputtedSOs;
    [SerializeField] private SpriteRenderer combined;

    [SerializeField] private List<SmoothieSO> smoothieRecipes = new List<SmoothieSO>();

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

            GameObject doneSmoothie = CheckSmoothie(inputtedSOs);
            if (doneSmoothie != null)
            {
                Instantiate(doneSmoothie, transform.position, Quaternion.identity);
                Debug.Log(doneSmoothie.name);
            }
            foreach (SpriteRenderer ingredient in inputtedIngredients)
            {

                if (ingredient.enabled)
                {
                    ingredient.enabled = false;
                }

            }

            for (int i = 0; i < inputtedSOs.Count; i++)
            {
                inputtedSOs.RemoveAt(i);
            }

            isFilled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            inputtedSOs.Add(collision.gameObject.GetComponent<FruitInfo>().ingredientSO);
            Debug.Log(inputtedIngredients[0]);
            foreach(SpriteRenderer ingredient in inputtedIngredients)
            {

                if (!ingredient.enabled)
                {
                    ingredient.color = collision.gameObject.GetComponent<FruitInfo>().fruitColor;
                    ingredient.enabled = true;
                    Destroy(collision.gameObject);
                    isFilled = true;
                    break;
                }

            }
        }
    }

    private GameObject CheckSmoothie(List<IngredientSO> playerBlend)
    {
        HashSet<IngredientSO> playerIngredients = new HashSet<IngredientSO>(playerBlend);

        foreach (var recipe in smoothieRecipes)
        {
            HashSet<IngredientSO> recipeIngredients = new HashSet<IngredientSO>(recipe.ingredients);

            if (playerIngredients.SetEquals(recipeIngredients))     
            {
                return recipe.prefab;
            }
        }
        return null;
    }
}