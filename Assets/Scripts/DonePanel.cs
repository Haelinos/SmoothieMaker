using UnityEngine;

public class DonePanel : MonoBehaviour
{
    public GenerateOrder[] orderGenerators; // Reference to all order generators

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Smoothie")) // Check if the collided object has the "Smoothie" tag
        {
            Smoothie smoothie = collision.GetComponent<Smoothie>();
            if (smoothie != null)
            {
                smoothie.CheckOrder(orderGenerators); // Check if the smoothie matches any order
                Destroy(collision.gameObject); // Destroy the smoothie after checking
            }
        }
    }
}