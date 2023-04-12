using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Playerjumping : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressBufferTime = .05f;

    Player player;

    bool tryingToJump;

    float lastJumpPressTime;
    
 
       

// Update is called once per frame
    void Awake()
    {
        player = GetComponent<Player>();
 
  
 
    }

    void OnEnable()
    {
       player.OnBeforeMove += OnBeforeMove;

    }
    
    void OnDisable()
    {
        player.OnBeforeMove -= OnBeforeMove;
    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }



    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;



        if( tryingToJump && player.IsGrounded)
        {
            player.velocity.y += jumpSpeed;
        }

        tryingToJump = false;
        
    }



































}
