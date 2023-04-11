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

    //allows us to change speed variable in inspector even it is private to read/write
    [SerializeField]

    public float distanceToCheck = 0.3f;

    public float speed = 3.5f;

    public float jumpspeed = 7f;

    public bool isGrounded;

    private float direction = 0f;

    private SpriteRenderer sprite;

    private Rigidbody2D player;

    public LayerMask groundlayer;



   

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();

       


        //For the animator controller
        

    }



    // Update is called once per frame
    void Update()
    {

        direction = Input.GetAxis("Horizontal");


        if (direction > 0f && Input.GetKey(KeyCode.D))
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            //_animator.SetTrigger("walk");
        }

        else if (direction < 0f && Input.GetKey(KeyCode.A))
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            //_animator.SetTrigger("walk");
        }

        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }


        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheck, groundlayer))
        {
            isGrounded = true;

        }

        else
        {
            isGrounded = false;

        }

       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            player.velocity = new Vector2(player.velocity.x, jumpspeed);

            isGrounded = false;

        }





    }


}