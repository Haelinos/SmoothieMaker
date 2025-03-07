using UnityEngine;

public class Glass : MonoBehaviour
{
    // Reference to the SmoothieMaker script
    public SmoothieMaker smoothieMaker;

    // Array to hold the 3 ingredient sprites (for the panels)
    public SpriteRenderer[] ingredientPanels;

    // List of ingredients in the glass
    private SmoothieMaker.Ingredient[] ingredients = new SmoothieMaker.Ingredient[3];
    private int ingredientCount = 0;

    // Dictionary to map ingredients to their sprites
    public IngredientSpriteMapping[] ingredientSpriteMappings;

    [System.Serializable]
    public class IngredientSpriteMapping
    {
        public SmoothieMaker.Ingredient ingredient;
        public Sprite sprite;
    }

    // Add an ingredient to the glass
    public void AddIngredient(SmoothieMaker.Ingredient ingredient)
    {
        if (ingredientCount < 3)
        {
            ingredients[ingredientCount] = ingredient;
            UpdateIngredientPanel(ingredientCount, ingredient); // Update the corresponding panel
            ingredientCount++;
            Debug.Log("Added ingredient to glass: " + ingredient);

            // Check if the glass is full
            if (ingredientCount == 3)
            {
                SendIngredientsToMixer();
            }
        }
        else
        {
            Debug.Log("Glass is full! Cannot add more ingredients.");
        }
    }

    // Update the sprite of a specific ingredient panel
    private void UpdateIngredientPanel(int panelIndex, SmoothieMaker.Ingredient ingredient)
    {
        // Find the sprite for the ingredient
        foreach (var mapping in ingredientSpriteMappings)
        {
            if (mapping.ingredient == ingredient)
            {
                ingredientPanels[panelIndex].sprite = mapping.sprite;
                break;
            }
        }
    }

    // Send the ingredients to the SmoothieMaker
    private void SendIngredientsToMixer()
    {
        Debug.Log("Sending ingredients to mixer...");

        // Add each ingredient to the SmoothieMaker
        foreach (var ingredient in ingredients)
        {
            smoothieMaker.AddIngredient(ingredient);
        }

        // Clear the glass
        ClearGlass();
    }

    // Clear the glass
    private void ClearGlass()
    {
        ingredients = new SmoothieMaker.Ingredient[3];
        ingredientCount = 0;

        // Clear the ingredient panels
        foreach (var panel in ingredientPanels)
        {
            panel.sprite = null;
        }

        Debug.Log("Glass cleared.");
    }

    // Detect collisions with ingredients
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has an IngredientObject component
        IngredientObject ingredientObject = collision.GetComponent<IngredientObject>();
        if (ingredientObject != null)
        {
            AddIngredient(ingredientObject.ingredient);
            Destroy(collision.gameObject); // Destroy the ingredient object after adding it
        }
    }
}