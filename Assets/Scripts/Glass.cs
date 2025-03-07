using UnityEngine;

public class Glass : MonoBehaviour
{

    public SmoothieMaker smoothieMaker;
    public SpriteRenderer[] ingredientPanels;
    private SmoothieMaker.Ingredient[] ingredients = new SmoothieMaker.Ingredient[3];
    private int ingredientCount = 0;
    public IngredientSpriteMapping[] ingredientSpriteMappings;
    public MouseDrag mouseDragScript;

    [System.Serializable]
    public class IngredientSpriteMapping
    {
        public SmoothieMaker.Ingredient ingredient;
        public Sprite sprite;
    }

    public void AddIngredient(SmoothieMaker.Ingredient ingredient)
    {
        if (ingredientCount < 3)
        {
            ingredients[ingredientCount] = ingredient;
            UpdateIngredientPanel(ingredientCount, ingredient);
            ingredientCount++;
            Debug.Log("Added ingredient to glass: " + ingredient);

            if (ingredientCount == 3)
            {
                if (mouseDragScript != null)
                {
                    mouseDragScript.canDrag = true;
                    Debug.Log("Glass is full. Dragging enabled.");
                }
            }
        }
        else
        {
            Debug.Log("Glass is full! Cannot add more ingredients.");
        }
    }
    private void UpdateIngredientPanel(int panelIndex, SmoothieMaker.Ingredient ingredient)
    {
        foreach (var mapping in ingredientSpriteMappings)
        {
            if (mapping.ingredient == ingredient)
            {
                ingredientPanels[panelIndex].sprite = mapping.sprite;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IngredientObject ingredientObject = collision.GetComponent<IngredientObject>();
        if (ingredientObject != null && ingredientCount < 3)
        {
            AddIngredient(ingredientObject.ingredient);
            Destroy(collision.gameObject); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SmoothieMixer"))
        {
            Debug.Log("Glass collided with the smoothie mixer!");
            if (ingredientCount == 3)
            {
                SmoothieMaker.SmoothieRecipe matchedRecipe = smoothieMaker.FindMatchingRecipe(ingredients);
                if (matchedRecipe != null)
                {
                    Instantiate(matchedRecipe.smoothiePrefab, collision.transform.position, Quaternion.identity);
                    Debug.Log("Smoothie created: " + matchedRecipe.smoothieName);
                }
                else
                {
                    Debug.Log("No matching smoothie recipe found!");
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Glass does not have 3 ingredients yet.");
            }
        }
    }
}