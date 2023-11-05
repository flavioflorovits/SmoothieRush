using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFilled = false;
    [SerializeField] private SpriteRenderer[] inputtedIngredients;

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
            foreach (SpriteRenderer ingredient in inputtedIngredients)
            {

                if (ingredient.enabled)
                {
                    ingredient.enabled = false;
                }

            }
            isFilled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("Ingredient"))
        {
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
}