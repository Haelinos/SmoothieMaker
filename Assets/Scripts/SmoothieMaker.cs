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
        public string smoothieName; 
        public List<Ingredient> requiredIngredients;
        public GameObject smoothiePrefab;
    }

    public List<SmoothieRecipe> smoothieRecipes = new List<SmoothieRecipe>();

    public SmoothieRecipe FindMatchingRecipe(SmoothieMaker.Ingredient[] ingredients)
    {
        foreach (var recipe in smoothieRecipes)
        {
            if (IsRecipeMatch(recipe, ingredients))
            {
                return recipe;
            }
        }
        return null;
    }

    private bool IsRecipeMatch(SmoothieRecipe recipe, SmoothieMaker.Ingredient[] ingredients)
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
                return false;
            }
        }
        return true;
    }
}