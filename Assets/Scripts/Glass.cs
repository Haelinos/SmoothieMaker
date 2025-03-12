using UnityEngine;

public class Glass : MonoBehaviour
{
    public SmoothieMaker smoothieMaker; // Reference to the SmoothieMaker
    public SpriteRenderer[] ingredientPanels; // Panels to display ingredient sprites
    private SmoothieMaker.Ingredient[] ingredients = new SmoothieMaker.Ingredient[3]; // Array to hold ingredients
    private int ingredientCount = 0; // Counter for ingredients in the glass
    public IngredientSpriteMapping[] ingredientSpriteMappings; // Maps ingredients to their sprites
    public MouseDrag mouseDragScript; // Reference to the MouseDrag script

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
            ingredients[ingredientCount] = ingredient; // Add the ingredient to the array
            UpdateIngredientPanel(ingredientCount, ingredient); // Update the corresponding panel
            ingredientCount++;
            Debug.Log("Added ingredient to glass: " + ingredient);

            // If the glass is full, enable dragging
            if (ingredientCount == 3)
            {
                if (mouseDragScript != null)
                {
                    mouseDragScript.canDrag = true; // Enable dragging
                    Debug.Log("Glass is full. Dragging enabled.");
                }
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
        if (ingredientPanels[panelIndex] == null)
        {
            Debug.LogError($"Ingredient panel at index {panelIndex} is null!");
            return;
        }

        foreach (var mapping in ingredientSpriteMappings)
        {
            if (mapping.ingredient == ingredient)
            {
                ingredientPanels[panelIndex].sprite = mapping.sprite; // Set the sprite
                break;
            }
        }
    }

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("Rigidbody2D component missing on Glass object!");
        }
        else
        {
            rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            Debug.Log("Glass Rigidbody2D found. Collision detection set to Continuous.");
        }

        Collider2D[] colliders = GetComponents<Collider2D>();
        if (colliders.Length == 0)
        {
            Debug.LogError("No Collider2D components found on Glass object!");
        }
        else
        {
            foreach (var collider in colliders)
            {
                Debug.Log($"Collider found on Glass. IsTrigger: {collider.isTrigger}");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger collision detected with: {collision.gameObject.name}");
        IngredientObject ingredientObject = collision.GetComponent<IngredientObject>();
        if (ingredientObject != null && ingredientCount < 3)
        {
            AddIngredient(ingredientObject.ingredient);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Physical collision detected with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("SmoothieMixer"))
        {
            Debug.Log($"Glass collided with SmoothieMixer. Ingredients: {ingredientCount}");
            if (ingredientCount == 3)
            {
                SmoothieMaker.SmoothieRecipe matchedRecipe = smoothieMaker.FindMatchingRecipe(ingredients);
                if (matchedRecipe != null)
                {
                    Instantiate(matchedRecipe.smoothiePrefab, collision.transform.position, Quaternion.identity);
                    Debug.Log($"Smoothie created: {matchedRecipe.smoothieName}");
                }
                else
                {
                    Debug.Log("No matching smoothie recipe found!");
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Glass does not have 3 ingredients yet. Current count: {ingredientCount}");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log($"Ongoing collision with: {collision.gameObject.name}");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"Collision ended with: {collision.gameObject.name}");
    }


}