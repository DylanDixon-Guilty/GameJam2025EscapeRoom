using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 movement;
    [SerializeField] private const string moveHorizontal = "Horizontal";
    [SerializeField] private const string moveVertical = "Vertical";
    [SerializeField] private const string cameraRotateAxis = "Mouse X";


    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw(moveHorizontal);
        float vertical = Input.GetAxisRaw(moveVertical);
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float rotateInput = Input.GetAxis(cameraRotateAxis);
        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
    }
}
