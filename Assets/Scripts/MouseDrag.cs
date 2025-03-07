using UnityEngine;

public class DragObject : MonoBehaviour
{

    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Debug.Log("Dragging");
            // Get the mouse position in screen space and convert it to world space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // Set Z to the camera's negative Z position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Update the object's position with the offset
            transform.position = new Vector3(worldPosition.x + offset.x, worldPosition.y + offset.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        isDragging = false;
    }
}