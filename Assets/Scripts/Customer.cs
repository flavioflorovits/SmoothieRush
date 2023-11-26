using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{

    public SmoothieSO smoothieSObject;
    public SmoothieInfo smoothieInfo;

    private bool accepted = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Customer collided with something");
        if (collision.gameObject.CompareTag("Smoothie")) 
        {
            Debug.Log("It was a smoothie");
            Debug.Log(smoothieSObject);
            Debug.Log(collision.gameObject.GetComponent<SmoothieInfo>().smoothieSO);
            if (!accepted && collision.gameObject.GetComponent<SmoothieInfo>().smoothieSO == smoothieSObject)
            {
                Debug.Log("it was accepted");
                accepted = true;
                Debug.Log("Correct smoothie");
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
    IEnumerator WaitDisable(GameObject smoothieObject)
    {
        Destroy(smoothieObject);
        yield return new WaitForSeconds(Random.Range(3, 7));
        transform.Find("Order").gameObject.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;

    }

}
