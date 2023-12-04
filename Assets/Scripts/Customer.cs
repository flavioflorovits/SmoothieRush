using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{

    public SmoothieSO smoothieSObject;
    public SmoothieInfo smoothieInfo;
    public GameObject tickObject;

    private bool accepted = false;
    private Image customerImage;

    private void Start()
    {
        customerImage = GetComponent<Image>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Customer collided with something");
        if (collision.gameObject.CompareTag("Smoothie")) 
        {
            Debug.Log("It was a smoothie");
            Debug.Log(smoothieSObject);
            Debug.Log(collision.gameObject.GetComponent<SmoothieInfo>().smoothieSO);
            SmoothieSO inputtedSObject = collision.gameObject.GetComponent<SmoothieInfo>().smoothieSO;
            if (!accepted && inputtedSObject == smoothieSObject)
            {
                Debug.Log("it was accepted");
                accepted = true;
                Debug.Log("Correct smoothie");
                DayManager.Instance.PaySmoothie(inputtedSObject.smoothiePrice);
                EnableTick();
                StartCoroutine(WaitDisable(collision.gameObject));
            }

        }

    }

    public void EnableIngredients()
    {
        accepted = false;
        for (int i = 0; i < smoothieInfo.orderIngredients.Length; i++)
        {
            Image image = smoothieInfo.orderIngredients[i];
            if (image.sprite != null)
            {
                smoothieInfo.orderIngredients[i].gameObject.SetActive(true);
            }
        }
        smoothieSObject = smoothieInfo.smoothieSO;
    }

    public void EnableTick()
    {
        for (int i = 0; i < smoothieInfo.orderIngredients.Length; i++)
        {
            Image image = smoothieInfo.orderIngredients[i];
            if (image.sprite != null)
            {
                smoothieInfo.orderIngredients[i].gameObject.SetActive(false);
            }
        }
        tickObject.SetActive(true);
    }

    IEnumerator WaitDisable(GameObject smoothieObject)
    {
        Destroy(smoothieObject);
        yield return new WaitForSeconds(Random.Range(3, 7));
        transform.Find("Order").gameObject.SetActive(false);
        customerImage.enabled = false;
        tickObject.SetActive(false);
    }

}
