using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public bool canDrag = true; // Allow dragging by default
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        if (canDrag)
        {
            // Calculate the offset between the mouse position and the object's position
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && canDrag)
        {
            // Move the object to the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        // Stop dragging when the mouse button is released
        isDragging = false;
    }
}