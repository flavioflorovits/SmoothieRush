using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class IngredientTouch : MonoBehaviour
{

    [SerializeField] private GameObject[] ingredientPrefabs;
    private GameObject clonedObject;
    private Camera mainCamera;
    private bool isDragging = false;

    private int originalOrder;

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
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {

                case TouchPhase.Began:
                    // Raycast to check if an ingredient is tapped
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.CompareTag("Ingredient"))
                    {
                        // Check the prefab of the touches ingredient
                        GameObject prefabToClone = IdentifyPrefab(hit.collider.gameObject);
                        if(prefabToClone != null)
                        {
                            clonedObject = Instantiate(prefabToClone, touchPosition, Quaternion.identity);
                            originalOrder = clonedObject.GetComponent<SpriteRenderer>().sortingOrder;
                            clonedObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
                            isDragging = true;
                        }

                    }
                 break;

                case TouchPhase.Moved:
                    if (isDragging && clonedObject != null)
                    {

                        clonedObject.transform.position = new Vector3(touchPosition.x, touchPosition.y, clonedObject.transform.position.z);

                    }
                break;

                case TouchPhase.Ended:
                    isDragging = false;
                    if (clonedObject != null)
                    {
                        clonedObject.GetComponent<SpriteRenderer>().sortingOrder = originalOrder;
                        Destroy(clonedObject);
                    }
                break;
                case TouchPhase.Canceled:
                    isDragging = false;
                    if (clonedObject != null)
                    {
                        clonedObject.GetComponent<SpriteRenderer>().sortingOrder = originalOrder;
                        Destroy(clonedObject);
                    }
                break;
            }

        }
    }


    public GameObject IdentifyPrefab(GameObject touchedObject)
    {

        // Check for the prefab
        foreach (GameObject prefab in ingredientPrefabs)
        {

            if (prefab.name == touchedObject.name) return prefab;

        }
        return null;
    }

}
