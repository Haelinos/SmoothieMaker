using UnityEngine;
using System.Collections.Generic;

public class Smoothie : MonoBehaviour
{
    public SmoothieMaker.Ingredient[] ingredients; // Stores the ingredients of this smoothie

    // Method to check if this smoothie matches any active order
    public void CheckOrder(GenerateOrder[] orderGenerators)
    {
        Debug.Log("Checking smoothie ingredients against orders...");

        // Print smoothie ingredients
        string smoothieIngredients = "Smoothie Ingredients: ";
        foreach (var ingredient in ingredients)
        {
            smoothieIngredients += ingredient + " ";
        }
        Debug.Log(smoothieIngredients);

        foreach (var orderGenerator in orderGenerators)
        {
            // Print order ingredients
            string orderIngredients = "Order Ingredients: ";
            foreach (var ingredient in orderGenerator.currentOrderRecipe)
            {
                orderIngredients += ingredient + " ";
            }
            Debug.Log(orderIngredients);

            if (orderGenerator.currentOrderRecipe.Count == ingredients.Length)
            {
                Debug.Log("Order recipe count matches smoothie ingredients count.");

                // Create a copy of the order ingredients list
                List<SmoothieMaker.Ingredient> remainingIngredients = new List<SmoothieMaker.Ingredient>(orderGenerator.currentOrderRecipe);

                bool isMatch = true;
                foreach (var ingredient in ingredients)
                {
                    if (!remainingIngredients.Contains(ingredient))
                    {
                        Debug.Log($"Mismatch: Smoothie has {ingredient}, but it's not in the order.");
                        isMatch = false;
                        break;
                    }
                    else
                    {
                        // Remove the matched ingredient from the remaining list
                        remainingIngredients.Remove(ingredient);
                    }
                }

                if (isMatch)
                {
                    Debug.Log("Order matched! Awarding 10 points.");
                    GameManager.Instance.AddPoints(10);

                    // Update the money UI
                    Money_UI moneyUI = FindFirstObjectByType<Money_UI>();
                    if (moneyUI != null)
                    {
                        moneyUI.UpdateMoneyUI();
                    }
                    else
                    {
                        Debug.LogError("Money_UI not found in the scene!");
                    }

                    return;
                }
            }
        }

        Debug.Log("No matching order found.");
    }
}