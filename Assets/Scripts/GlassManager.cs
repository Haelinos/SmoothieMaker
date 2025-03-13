using UnityEngine;

public class GlassManager : MonoBehaviour
{
    public GameObject glassPrefab; // Assign the glass prefab in the Inspector
    public Vector3 spawnPosition; // Position to respawn the glass

    // Call this method to respawn the glass
    public void RespawnGlass()
    {
        Instantiate(glassPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("New glass spawned at: " + spawnPosition);
    }
}