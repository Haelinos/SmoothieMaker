using UnityEngine;
using System.Collections.Generic;

public class SmoothieMaker : MonoBehaviour
{
    public enum Ingredient
    {
        Spinach, Banana, Blueberry, Mango, Strawberry
    }

    [System.Serializable]
    public class SmoothieRecipe
    {
        public string smoothieName; // Name of the smoothie
        public List<Ingredient> requiredIngredients; // Ingredients needed for this smoothie
        public GameObject smoothiePrefab; // Prefab to instantiate for this smoothie
    }

    public List<SmoothieRecipe> smoothieRecipes = new List<SmoothieRecipe>(); // List of smoothie recipes

    // Find a matching recipe based on the ingredients
    public SmoothieRecipe FindMatchingRecipe(Ingredient[] ingredients)
    {
        foreach (var recipe in smoothieRecipes)
        {
            if (IsRecipeMatch(recipe, ingredients))
            {
                return recipe; // Return the matching recipe
            }
        }
        return null; // No matching recipe found
    }

    // Check if the selected ingredients match a specific recipe
    private bool IsRecipeMatch(SmoothieRecipe recipe, Ingredient[] ingredients)
    {
        foreach (var requiredIngredient in recipe.requiredIngredients)
        {
            bool ingredientFound = false;
            foreach (var ingredient in ingredients)
            {
                if (ingredient == requiredIngredient)
                {
                    ingredientFound = true;
                    break;
                }
            }
            if (!ingredientFound)
            {
                return false; // Required ingredient not found
            }
        }
        return true; // All required ingredients are present
    }
}