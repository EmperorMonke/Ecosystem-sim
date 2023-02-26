using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    void Update()
    {
        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the camera position based on input
        Vector3 newPosition = transform.position;
        newPosition += transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        newPosition += transform.right * horizontalInput * moveSpeed * Time.deltaTime;
        transform.position = newPosition;

        // Rotate the camera based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(-mouseY, mouseX, 0.0f);
    }
}