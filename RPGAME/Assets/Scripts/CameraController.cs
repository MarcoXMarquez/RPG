using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public Tilemap Tilemap; // Reference to the Tilemap
    private Vector3 bottomLeftLimit; // Bottom-left limit of the camera
    private Vector3 topRightLimit; // Top-right limit of the camera

    private float halfHeight;
    private float halfWidth;        
    void Start()
    {

        target = PlayerController2D.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = Tilemap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = Tilemap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        PlayerController2D.instance.SetBounds(Tilemap.localBounds.min, Tilemap.localBounds.max);
    }

    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Clamp the camera's position to the tilemap bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x + 0.5f, topRightLimit.x - 0.5f),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y + 0.5f, topRightLimit.y - 0.5f),
            transform.position.z
        );
    }
}
