using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public void SetCameraPosition(Vector3 position)
    {
        transform.position = position;
    }
}
