using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;

    [SerializeField] float movementSpeed = 5f;

    [SerializeField] float jumpSpeed = 5f;

    [SerializeField] float mass = 1f;

    [SerializeField] float acceleration = 20f;

    public Transform cameraTransform;

    public bool IsGrounded => controller.isGrounded;

    bool wasGrounded;

    CharacterController controller;

    internal Vector3 velocity;

    Vector2 look;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction sprintAction;


    public float Height
    {
        get => controller.height;
        set => controller.height = value;
    }




    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;
    internal float movementSpeedMultiplier;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        sprintAction = playerInput.actions["sprint"];
    }



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        UpdateGround();
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = IsGrounded ? -1f : velocity.y + gravity.y;

    }


    void UpdateGround()
    {
        if( wasGrounded != IsGrounded)
        {
            OnGroundStateChange?.Invoke(IsGrounded);
            wasGrounded = IsGrounded;
        }
    }



    Vector3 GetMovementInput()
    {
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();

        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);

        //var sprintInput = sprintAction.ReadValue<float>();

        //var multiplier = sprintInput > 0 ? 2f : 1f;

        input *= movementSpeed * movementSpeedMultiplier;
        return input;
    }




    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();

        var input = GetMovementInput();
        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

       

        controller.Move((velocity * Time.deltaTime));
    }



   
    // Update is called once per frame
    void UpdateLook()
    {
        var LookInput = lookAction.ReadValue<Vector2>();
        look.x += LookInput.x * mouseSensitivity;
        look.y += LookInput.y * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);

        transform.localRotation = Quaternion.Euler(0, look.x, 0);

    }
}
