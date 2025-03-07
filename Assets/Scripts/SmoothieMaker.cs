using UnityEngine;
using System.Collections.Generic;

public class SmoothieMaker : MonoBehaviour
{
    // Enum for ingredients
    public enum Ingredient
    {
        Spinach, Banana, Blueberry, Mango, Strawberry
    }

    // Smoothie class to define smoothie recipes
    [System.Serializable]
    public class SmoothieRecipe
    {
        public string smoothieName; // Name of the smoothie
        public List<Ingredient> requiredIngredients; // Ingredients needed for this smoothie
        public GameObject smoothiePrefab; // Unique prefab for this smoothie
    }

    private List<Ingredient> selectedIngredients = new List<Ingredient>();
    public List<SmoothieRecipe> smoothieRecipes = new List<SmoothieRecipe>();

    public void AddIngredient(Ingredient ingredient)
    {
        if (selectedIngredients.Count < 3)
        {
            selectedIngredients.Add(ingredient);
            Debug.Log("Added ingredient: " + ingredient);

            // Check if 3 ingredients are selected
            if (selectedIngredients.Count == 3)
            {
                CheckForSmoothie();
            }
        }
        else
        {
            Debug.Log("Already selected 3 ingredients!");
        }
    }

    private void CheckForSmoothie()
    {
        foreach (var recipe in smoothieRecipes)
        {
            if (IsRecipeMatch(recipe))
            {
                Debug.Log("Smoothie created: " + recipe.smoothieName);
                Instantiate(recipe.smoothiePrefab, Vector3.zero, Quaternion.identity); // Instantiate the unique prefab
                selectedIngredients.Clear(); // Reset the selection
                return;
            }
        }

        Debug.Log("No matching smoothie recipe found!");
        selectedIngredients.Clear(); // Reset the selection
    }

    private bool IsRecipeMatch(SmoothieRecipe recipe)
    {
        // Check if the selected ingredients match the required ingredients
        foreach (var ingredient in recipe.requiredIngredients)
        {
            if (!selectedIngredients.Contains(ingredient))
            {
                return false;
            }
        }
        return true;
    }
}