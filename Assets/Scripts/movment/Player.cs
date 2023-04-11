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

    [SerializeField] Transform cameraTransform;

    CharacterController controller;

    Vector3 velocity;

    Vector2 look;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction sprintAction;

    public event Action OnBeforeMove;
    internal float movementSpeedMultiplier;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        sprintAction = playerInput.actions["sprint"];
    }



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        UpdateMovement();
        UpdateLook();
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

        var jumpInput = jumpAction.ReadValue<float>();
        if(jumpInput > 0 && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }

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
