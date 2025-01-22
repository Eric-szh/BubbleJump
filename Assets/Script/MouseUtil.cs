using UnityEngine;

public static class MouseUtil
{

    public static Vector3 getMosuePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set Z to the near clip plane of the camera
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0;

        return worldMousePosition;
    }

    public static Vector3 getMouseDirection(Vector3 position)
    {
        Vector3 direction = getMosuePosition() - position;
        direction.z = 0; // Ensure no rotation out of the 2D plane

        return direction;
    }
}
