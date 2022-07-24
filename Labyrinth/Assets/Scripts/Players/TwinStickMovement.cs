using System;
using System.Collections;
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
    private Animator animator;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private bool hasJumped = false;
    private float jumpMultiplicator = 2f;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    public Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
        if (transform.position.y < -2)
        {
            transform.localPosition = startPosition;
            return;
        }

        HandleInput();
        HandleMovement();
        HandleGravity();
        HandleRotation();
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (swordColliderDuration > 0f)
        {
            swordColliderDuration -= Time.deltaTime;
        }
        else if (swordCollider.enabled)
        {
            swordCollider.enabled = false;
        }
    }


    private void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    private Vector3 move;

    private void HandleMovement()
    {
        jumpTimer -= Time.deltaTime;

        move = new(movement.x, 0, movement.y);
        controller.Move(playerSpeed * Time.deltaTime * move);

        float angle = Vector3.SignedAngle(transform.forward, move, Vector3.up);

        //Multiply by magnitude to avoid movement animation when there is none
        animator.SetFloat("Forward", Mathf.Cos(angle * Mathf.Deg2Rad) * move.magnitude);
        animator.SetFloat("Right", Mathf.Sin(angle * Mathf.Deg2Rad) * move.magnitude);
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
#pragma warning disable IDE0079 // Retirer la suppression inutile
#pragma warning disable IDE0051 // Supprimer les membres privés non utilisés

    private float jumpDuration = 0.3f;
    private float jumpTimer = 0f;
    private void OnJump()
    {
        if (!hasJumped && jumpTimer <= 0f)
        {
            hasJumped = true;
            jumpTimer = jumpDuration;
            animator.SetTrigger("Jump");
            playerVelocity.y = Mathf.Sqrt(playerJumpHeight * -3.0f * gravityValue * jumpMultiplicator);
        }
    }

    private float attackAnimDuration = 0.2f;
    private float swordColliderDuration = 0f;
    [SerializeField] private Collider swordCollider;

    private void OnAttack()
    {
        animator.SetTrigger("Attack");
        swordColliderDuration = attackAnimDuration;
        StartCoroutine(DelayedSwordActivation());
    }

    private IEnumerator DelayedSwordActivation()
    {
        yield return new WaitForSeconds(0.04f);
        swordCollider.enabled = true;
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad");
    }
#pragma warning restore IDE0051 // Supprimer les membres privés non utilisés
#pragma warning restore IDE0079 // Retirer la suppression inutile
    #endregion PlayerInput message handlers
}
