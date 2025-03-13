using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateOrder : MonoBehaviour
{
    public GameObject[] ingredients; // Assign ingredients in Unity Inspector
    public int orderTimer; // Time between orders
    private Vector3[] positions;
    public List<GameObject> currentOrder = new List<GameObject>(); // Store current order
    public List<SmoothieMaker.Ingredient> currentOrderRecipe = new List<SmoothieMaker.Ingredient>(); // Store current order recipe

    private void Start()
    {
        // Define ingredient positions
        positions = new Vector3[]
        {
            new Vector3(transform.position.x - 0.75f, transform.position.y - 0.25f, 0),
            new Vector3(transform.position.x, transform.position.y - 0.25f, 0),
            new Vector3(transform.position.x + 0.75f, transform.position.y - 0.25f, 0)
        };

        StartCoroutine(OrderGenerator()); // Start generating orders
    }

    IEnumerator OrderGenerator()
    {
        GameObject[][] smoothies = new GameObject[][]
        {
            new GameObject[] { ingredients[0], ingredients[0], ingredients[1] }, // Smoothie 1: Banana, Banana, Strawberry
            new GameObject[] { ingredients[4], ingredients[2], ingredients[0] }, // Smoothie 2: Spinach, Mango, Banana
            new GameObject[] { ingredients[0], ingredients[3], ingredients[3] }, // Smoothie 3: Banana, Blueberry, Blueberry
            new GameObject[] { ingredients[1], ingredients[3], ingredients[2] }  // Smoothie 4: Strawberry, Blueberry, Mango
        };

        while (true) // Infinite loop to keep generating orders
        {
            // Destroy previous order
            foreach (GameObject obj in currentOrder)
            {
                if (obj != null)
                {
                    Debug.Log($"Destroying ingredient: {obj.name}");
                    Destroy(obj);
                }
            }
            currentOrder.Clear(); // Clear the list
            currentOrderRecipe.Clear(); // Clear the recipe

            int randomSmoothie = Random.Range(0, smoothies.Length); // Pick a random smoothie
            Debug.Log("Spawning Smoothie Order: " + randomSmoothie);

            // Spawn new order
            for (int i = 0; i < smoothies[randomSmoothie].Length; i++)
            {
                GameObject ingredient = Instantiate(smoothies[randomSmoothie][i], positions[i], transform.rotation);
                currentOrder.Add(ingredient); // Save reference to destroy later

                // Manually assign the ingredient type after instantiation
                IngredientObject ingredientObject = ingredient.GetComponent<IngredientObject>();
                if (ingredientObject != null)
                {
                    // Assign the correct ingredient type from the prefab
                    ingredientObject.ingredient = smoothies[randomSmoothie][i].GetComponent<IngredientObject>().ingredient;

                    currentOrderRecipe.Add(ingredientObject.ingredient);
                    Debug.Log($"Assigned ingredient: {ingredientObject.ingredient} to {ingredient.name}");
                }
                else
                {
                    Debug.LogWarning($"IngredientObject component missing on {ingredient.name}");
                }

                Debug.Log($"Spawned ingredient: {ingredient.name} at position: {positions[i]}");
            }

            yield return new WaitForSeconds(orderTimer);

            // Destroy previous order AFTER waiting
            foreach (GameObject obj in currentOrder)
            {
                if (obj != null)
                {
                    Debug.Log($"Destroying ingredient: {obj.name}");
                    Destroy(obj);
                }
            }
            currentOrder.Clear(); // Clear the list
            currentOrderRecipe.Clear(); // Clear the recipe
        }
    }
}