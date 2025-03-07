using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateOrder : MonoBehaviour
{
    public GameObject[] ingredients;
    public int orderTimer;
    private Vector3[] positions;

    private void Start()
    {
        // Define ingredient positions
        positions = new Vector3[]
        {
            new Vector3(transform.position.x - 0.75f, transform.position.y - 0.25f, transform.position.z),
            new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z),
            new Vector3(transform.position.x + 0.75f, transform.position.y - 0.25f, transform.position.z)
        };

        StartCoroutine(OrderGenerator());
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
            int randomSmoothie = Random.Range(0, smoothies.Length); // Pick a random smoothie
            Debug.Log("Spawning Smoothie Order: " + randomSmoothie);

            // Spawn each ingredient directly in the coroutine
            for (int i = 0; i < smoothies[randomSmoothie].Length; i++)
            {
                Instantiate(smoothies[randomSmoothie][i], positions[i], transform.rotation);
            }

            yield return new WaitForSeconds(orderTimer); // Wait before spawning the next order
        }
    }
}
