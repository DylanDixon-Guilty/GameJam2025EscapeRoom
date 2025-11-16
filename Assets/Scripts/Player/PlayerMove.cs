using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 movement;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float cameraVerticalRotation = 0f;
    [SerializeField] private const string moveHorizontal = "Horizontal";
    [SerializeField] private const string moveVertical = "Vertical";
    [SerializeField] private const string mouseAxisX = "Mouse X";
    [SerializeField] private const string mouseAxisY = "Mouse Y";
    
    // This is for when the player is interacting with a puzzle.
    // If it is locked, the player can rotate and move around the world.
    // If it is not locked, do not let the player rotate or move the player.
    // Since they interacting with a puzzle and need the mouse.
    [SerializeField] private bool isCursorLocked = true;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Rotation();
    }

    private void Move()
    {
        if(isCursorLocked)
        {
            float horizontal = Input.GetAxisRaw(moveHorizontal);
            float vertical = Input.GetAxisRaw(moveVertical);
            movement.Set(horizontal, 0f, vertical);
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotation()
    {
        if(isCursorLocked)
        {
            float inputX = Input.GetAxis(mouseAxisX) * mouseSensitivity;
            float inputY = Input.GetAxis(mouseAxisY) * mouseSensitivity;

            // To rotate the camera up and down
            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            // To rotate the camera left and right
            player.Rotate(Vector3.up * inputX);
        }
    }
}
