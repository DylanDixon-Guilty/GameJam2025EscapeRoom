using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensitivityX = 2f;
    [SerializeField] private float mouseSensitivityY = 2f;

    [SerializeField] private const string mouseAxisX = "Mouse X";
    [SerializeField] private const string mouseAxisY = "Mouse Y";

    private float VerticalRotationY = 0f;
    private float HorizontalRotationX = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float inputX = Input.GetAxis(mouseAxisX) * mouseSensitivityX * Time.deltaTime;
        float inputY = Input.GetAxis(mouseAxisY) * mouseSensitivityY * Time.deltaTime;

        VerticalRotationY += inputX; //To rotate camera left and right
        HorizontalRotationX -= inputY; //To rotate camera up and Down
        HorizontalRotationX = Mathf.Clamp(HorizontalRotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(HorizontalRotationX, VerticalRotationY, 0);
        player.rotation = Quaternion.Euler(0, VerticalRotationY, 0);
        //Rotation();
    }

    private void Rotation()
    {
        float inputX = Input.GetAxis(mouseAxisX) * mouseSensitivityX * Time.deltaTime;
        float inputY = Input.GetAxis(mouseAxisY) * mouseSensitivityY * Time.deltaTime;

        VerticalRotationY += inputX; //To rotate camera left and right
        HorizontalRotationX -= inputY; //To rotate camera up and Down
        HorizontalRotationX = Mathf.Clamp(HorizontalRotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(HorizontalRotationX, VerticalRotationY, 0);
        player.rotation = Quaternion.Euler(0, VerticalRotationY, 0);
    }
}
