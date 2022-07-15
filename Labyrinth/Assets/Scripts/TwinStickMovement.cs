using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class TwinStickMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerJumpHeight = 1f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private bool hasJumped = false;
    private float jumpMultiplicator = 2f;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleGravity();
        HandleRotation();
    }

    private void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        Vector3 move = new(movement.x, 0, movement.y);
        controller.Move(playerSpeed * Time.deltaTime * move);
    }

    private void HandleGravity()
    {
        //Fix characterController.isGrounded detection
        controller.Move(-0.1f * Time.deltaTime * Vector3.up);

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
            hasJumped = false;

        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        playerVelocity.y += gravityValue * 3f * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (isGamepad)
        {
            if (Mathf.Abs(aim.x) > controllerDeadzone ||
                Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane playerPlane = new(Vector3.up, transform.position);
            if (playerPlane.Raycast(ray, out float hitDistance))
                LookAt(ray.GetPoint(hitDistance));
        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    #region PlayerInput message handlers
#pragma warning disable IDE0051 // Supprimer les membres privés non utilisés
    private void OnJump()
    {
        if (!hasJumped)
        {
            hasJumped = true;
            playerVelocity.y = Mathf.Sqrt(playerJumpHeight * -3.0f * gravityValue * jumpMultiplicator);
        }
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad");
    }
#pragma warning restore IDE0051 // Supprimer les membres privés non utilisés
    #endregion PlayerInput message handlers
}
