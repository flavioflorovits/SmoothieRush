using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayNumber;

    [SerializeField] private TextMeshProUGUI moneyEarned;
    [SerializeField] private TextMeshProUGUI moneySpent;
    [SerializeField] private TextMeshProUGUI totalMoney;

    [SerializeField] private TextMeshProUGUI smoothiesMade;
    [SerializeField] private TextMeshProUGUI smoothiesBotched;
    [SerializeField] private TextMeshProUGUI ingredientsUsed;


    public IEnumerator DisplayStats(int day, float earned, float spent, float money, int made, int botched, int ingredients)
    {
        dayNumber.text = "Day " + day.ToString();

        // Round the money values to the nearest decimal
        earned = Mathf.Round(earned * 10) / 10f;
        spent = Mathf.Round(spent * 10) / 10f;
        money = Mathf.Round(money * 10) / 10f;

        // Format money values with commas
        string formattedEarned = "€ " + earned.ToString("N1");
        string formattedSpent = "€ " + spent.ToString("N1");
        string formattedTotalMoney = "€ " + money.ToString("N1");

        // Display money earned with delay
        yield return DisplayWithDelay(moneyEarned, formattedEarned, 0.5f);

        // Display money spent with delay
        yield return DisplayWithDelay(moneySpent, formattedSpent, 0.5f);

        // Display total money with delay
        yield return DisplayWithDelay(totalMoney, formattedTotalMoney, 0.5f);

        // Display smoothies made with delay
        yield return DisplayWithDelay(smoothiesMade, made.ToString(), 0.5f);

        // Display smoothies botched with delay
        yield return DisplayWithDelay(smoothiesBotched, botched.ToString(), 0.5f);

        // Display ingredients used with delay
        yield return DisplayWithDelay(ingredientsUsed, ingredients.ToString(), 0.5f);
    }

    // Helper method to display text with delay
    private IEnumerator DisplayWithDelay(TextMeshProUGUI text, string value, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = value;
    }




}
