using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateOrder : MonoBehaviour
{
    public GameObject[] ingredients; // Assign ingredients in Unity Inspector
    public int orderTimer; // Time between orders
    private Vector3[] positions;
    private List<GameObject> currentOrder = new List<GameObject>(); // Store current order

    private void Start()
    {
        // Define ingredient positions
        positions = new Vector3[]
        {
            new Vector3(transform.position.x - 0.75f, transform.position.y - 0.25f, transform.position.z),
            new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z),
            new Vector3(transform.position.x + 0.75f, transform.position.y - 0.25f, transform.position.z)
        };

        StartCoroutine(OrderGenerator()); // Start generating orders
    }

    IEnumerator OrderGenerator()
    {
        GameObject[][] smoothies = new GameObject[][]
        {
            new GameObject[] { ingredients[0], ingredients[0], ingredients[1] }, // Smoothie 1: banana-banana-strawberries
            new GameObject[] { ingredients[4], ingredients[2], ingredients[0] }, // Smoothie 2: spinach-mango-banana
            new GameObject[] { ingredients[0], ingredients[3], ingredients[3] }, // Smoothie 3: banana-blueberries-blueberries
            new GameObject[] { ingredients[1], ingredients[3], ingredients[2] }  // Smoothie 4: strawberries-blueberries-mango
        };

        while (true) // Infinite loop to keep generating orders
        {
            // Destroys previous order
            foreach (GameObject obj in currentOrder)
            {
                Destroy(obj);
            }
            currentOrder.Clear(); // Clear the list

            int randomSmoothie = Random.Range(0, smoothies.Length); // Pick a random smoothie
            Debug.Log("Spawning Smoothie Order: " + randomSmoothie);

            // Spawns new order
            for (int i = 0; i < smoothies[randomSmoothie].Length; i++)
            {
                GameObject ingredient = Instantiate(smoothies[randomSmoothie][i], positions[i], transform.rotation);
                currentOrder.Add(ingredient); // Save reference to destroy later
            }
            yield return new WaitForSeconds(orderTimer);

            // Destroy previous order AFTER waiting
            foreach (GameObject obj in currentOrder)
            {
                Destroy(obj);
            }
            currentOrder.Clear(); // Clear the list
        }
    }
}