using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothieTouch : MonoBehaviour
{
    GameObject smoothieObject;

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
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Raycast to check if an ingredient is tapped
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.CompareTag("Smoothie"))
                    {
                        // Set the object that player touches
                        smoothieObject = hit.collider.gameObject;
                        if (smoothieObject != null)
                        {
                            originalOrder = smoothieObject.GetComponent<SpriteRenderer>().sortingOrder;
                            smoothieObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
                            isDragging = true;
                        }

                    }
                    break;
                case TouchPhase.Moved:
                    if (isDragging && smoothieObject != null)
                    {
                        smoothieObject.transform.position = new Vector3(touchPosition.x, touchPosition.y, smoothieObject.transform.position.z);
                    }
                break;
                case TouchPhase.Ended:
                    isDragging = false;
                    if (smoothieObject != null)
                    {
                        if (smoothieObject.transform.position.y > 0.9f)
                        {
                            Vector3 savedPosition = smoothieObject.transform.position;
                            smoothieObject.transform.position = new Vector3(savedPosition.x, 0.9f, savedPosition.z);
                        }
                        else if (smoothieObject.transform.position.y < -2.83f)
                        {
                            Vector3 savedPosition = smoothieObject.transform.position;
                            smoothieObject.transform.position = new Vector3(savedPosition.x, -2.83f, savedPosition.z);
                        }
                        smoothieObject.GetComponent<SpriteRenderer>().sortingOrder = originalOrder;
                    }
                break;
                case TouchPhase.Canceled:
                    isDragging = false;
                    if (smoothieObject != null)
                    {
                        if (smoothieObject.transform.position.y > 0.9f)
                        {
                            Vector3 savedPosition = smoothieObject.transform.position;
                            smoothieObject.transform.position = new Vector3(savedPosition.x, 0.9f, savedPosition.z);
                        }
                        else if(smoothieObject.transform.position.y < -2.83f)
                        {
                            Vector3 savedPosition = smoothieObject.transform.position;
                            smoothieObject.transform.position = new Vector3(savedPosition.x, -2.83f, savedPosition.z);
                        }
                        smoothieObject.GetComponent<SpriteRenderer>().sortingOrder = originalOrder;
                    }
                    break;

            }

        }
    }


}
