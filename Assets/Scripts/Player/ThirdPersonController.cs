using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    private PlayerStats playerStats;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jumping")]
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Flight")]
    public float flightForce = 20f;
    public float flightEnergyCostPerSecond = 20f;

    private bool isFlying;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float dashEnergyCost = 25f;

    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;

    private Vector3 dashDirection;

    [Header("Aiming")]
    public LayerMask aimMask;
    public float aimSmoothSpeed = 20f;

    private Vector3 currentAimPoint;
    // Exposes the aimpoint to other scripts
    public Vector3 CurrentAimPoint => currentAimPoint;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController controller;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private Vector3 velocity;

    private bool isGrounded;
    private bool isCursorLocked = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //inputActions = new PlayerInputActions();
        inputHandler = GetComponent<PlayerInputHandler>();
        playerStats = GetComponent<PlayerStats>();
        LockCursor();
    }

    private void OnEnable()
    {
        //inputActions.Player.Enable();

        //// Attach actions to functions
        //inputActions.Player.Move.performed += OnMove;
        //inputActions.Player.Move.canceled += OnMove;

        //inputActions.Player.Jump.performed += OnJump;
        //inputActions.Player.Dash.performed += OnDash;

        inputHandler.JumpPressed += Jump;
        inputHandler.DashPressed += StartDash;
    }

    private void OnDisable()
    {
        //inputActions.Player.Move.performed -= OnMove;
        //inputActions.Player.Move.canceled -= OnMove;

        //inputActions.Player.Jump.performed -= OnJump;
        //inputActions.Player.Dash.performed -= OnDash;

        //inputActions.Player.Disable();
        inputHandler.JumpPressed -= Jump;
        inputHandler.DashPressed -= StartDash;
    }

    private void Update()
    {
        CheckGrounded();

        HandleDashTimers();

        if (isDashing)
        {
            HandleDash();
            return;
        }
        UpdateAimPoint();
        RotateTowardsAim();

        HandleMovement();

        HandleFlight();

        HandleGravity();

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            toggleCursorLock(isCursorLocked);
        }
    }

    private void Start()
    {
        playerStats.Died += OnPlayerDied;
    }


    //TODO: This can't be the right place to handle player death here, can it?
    //Handle Player Death
    private void OnPlayerDied()
    {
        Debug.Log("Game Over");
    }

    // Used to Check if player is on the ground
    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        //if (isGrounded)
        //{
        //    Debug.Log("Am Grounded");
        //}

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    // Handle movement
    private void HandleMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Move player in inputted direction
        Vector3 moveDirection =
            forward * inputHandler.MoveInput.y +
            right * inputHandler.MoveInput.x;

        // Rotate Player in direction of the movement
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            //transform.rotation = Quaternion.Slerp(
            //    transform.rotation,
            //    targetRotation,
            //    rotationSpeed * Time.deltaTime
            //);
        }

        controller.Move(
            moveDirection * moveSpeed * Time.deltaTime
        );
    }

    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    // Jump
    private void Jump()
    {
        Debug.Log("Jump Pressed");

        // If player is touching the ground
        if (!isGrounded)
            return;

        // Actual Jumping
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void HandleFlight()
    {
        if (!inputHandler.IsJumpHeld)
            return;

        // Continuously drain player health
        if (!playerStats.ConsumeEnergy(
            flightEnergyCostPerSecond * Time.deltaTime))
            return;

        //Debug.Log("Am Flying");

        // TODO: Right now flight kinda messed up
        // Supposed to have some momentum/acceleration

        //velocity.y += flightForce * Time.deltaTime;
        velocity.y = flightForce;
    }

    private void StartDash()
    {
        if (isDashing)
            return;

        if (dashCooldownTimer > 0)
            return;

        if (!playerStats.ConsumeEnergy(dashEnergyCost))
            return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        dashDirection =
            forward * inputHandler.MoveInput.y +
            right * inputHandler.MoveInput.x;

        // If no movement input,
        // dash toward facing direction
        if (dashDirection.magnitude < 0.1f)
        {
            dashDirection = transform.forward;
        }

        dashDirection.Normalize();

        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
    }

    private void HandleDash()
    {
        controller.Move(
            dashDirection * dashSpeed * Time.deltaTime
        );

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            isDashing = false;
        }
    }

    private void HandleDashTimers()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void UpdateAimPoint()
    {
        Ray ray = Camera.main.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimMask))
        {
            currentAimPoint = hit.point;
        }
        else
        {
            currentAimPoint =
                ray.origin + ray.direction * 1000f;
        }

    }
    private void RotateTowardsAim()
    {
        Vector3 aimDirection =
            currentAimPoint - transform.position;

        aimDirection.y = 0f;

        if (aimDirection.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(aimDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            aimSmoothSpeed * Time.deltaTime
        );
    }

    private void LockCursor()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Make the cursor invisible
        Cursor.visible = false;
        isCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;

        // Make the cursor visible
        Cursor.visible = true;
        isCursorLocked = false;
    }

    private void toggleCursorLock(bool togg)
    {
        if (togg)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }
}