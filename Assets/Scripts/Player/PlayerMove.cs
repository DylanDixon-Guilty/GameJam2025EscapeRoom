using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    private float speedMultiplier = 10f;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float groundDrag = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private Transform player;
    private float horizontalInput;
    private float verticalInput;
    private const string movementX = "Horizontal";
    private const string movementY = "Vertical";
    Vector3 movementDirection;
    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
    }

    private void Update()
    {
        //This will check if the player is on the ground to ensure there is drag to the player
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MovementInput();
        SpeedControl();
        MovePlayer();

        //Handling drag of player if they are on ground (they should never leave the ground but just in case)
        if (isGrounded)
        {
            playerRB.linearDamping = groundDrag;
        }
        else
        {
            playerRB.linearDamping = 0;
        }
    }

    private void MovementInput()
    {
        horizontalInput = Input.GetAxis(movementX);
        verticalInput = Input.GetAxis(movementY);
    }

    private void MovePlayer()
    {
        movementDirection = player.forward * verticalInput + player.right * horizontalInput;
        playerRB.AddForce(movementDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Force);
    }

    /// <summary>
    /// Limits the players movement by what the variable is set to
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z);
        //Limit the velocity of player
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            playerRB.linearVelocity = new Vector3(limitedVelocity.x, playerRB.linearVelocity.y, limitedVelocity.z);
        }
    }
}
