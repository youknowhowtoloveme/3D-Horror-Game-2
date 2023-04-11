using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
    //allows us to change speed variable in inspector even it is private to read/write
    [SerializeField]

    
   // basic player paramaters
   public float walkSpeed = 3;

   public float runSpeed = 7;

   public float crouchSpeed = 2;

   public float proneSpeed = 1;

   public float jumpHeight = 7;
    
   public float stateChangeSpeed = 3f;
       
   public float runTransitionSpeed = 8f;
    


   // stamina paramaters
   public float playerMaxStamina = 1f;

   public float staminaRegenSpeed = 1f;

   public float runExhaustionSpeed = 1f;

   public float jumpExhaustion = 1f;

   public float regenerateAfter = 2f;

   public bool autoRegenerate = true;


    // CameraHeadBob
    public sealed class CameraHeadBob
    {

        public Animation cameraAnimations;

        public string cameraIdle = "CameraIdle";

        public string cameraWalk = "CameraWalk";

        public string cameraRun = "CameraRun";

        [Range(0, 5)] public float walkAnimSpeed = 1f;

        [Range(0, 5)] public float runAnimSpeed = 1f;
    }


    // ArmsHeadBob
    public sealed class ArmsHeadBob
    {
        public Animation armsAnimations;

        public string armsIdle = "ArmsIdle";

        public string armsBreath = "ArmsBreath";

        public string armsWalk = "ArmsWalk";

        public string armsRun = "ArmsRun";

        [Range(0, 5)] public float walkAnimSpeed = 1f;

        [Range(0, 5)] public float runAnimSpeed = 1f;

        [Range(0, 5)] public float breathAnimSpeed = 1f;
    }


    //ground settings
    public float groundCheckOffset;
    public float groundCheckRadius;
    public bool isGrounded;

    */

  
    [SerializeField]


    public float speed = 3.5f;

    private CharacterController controller;

    private Vector3 playerVelocity;

    private bool isGrounded;

    public float gravity = -9.8f;

    public float jumpHeight = 3f;

    bool crouching = false;
    
    float crouchTimer = 1;
    
    bool lerpCrouch = false;
    
    bool sprinting = false;

    public float sprint = 3f;




    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();

    }



    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

    }


    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }



    public void Jump()
    {
       
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        
    }

   

}