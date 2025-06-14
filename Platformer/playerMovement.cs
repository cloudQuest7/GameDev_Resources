using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    [SerializeField]float runSpeed = 10f;
    [SerializeField]float jumpSpeed = 5f;
    [SerializeField]float climbSpeed = 5f; 

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    float gravityScaleAtStart;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        climbLadder();
    }

    void OnJump(InputValue value)
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

        // if (value.isPressed && Mathf.Abs(myRigidbody.velocity.y) < Mathf.Epsilon)
        // {
        //     myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        // }
    }

    void climbLadder()
    {
         if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
         {
            myRigidbody.gravityScale = gravityScaleAtStart;
             myAnimator.SetBool("isClimbing", false);
            return;
         }
         Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
         myRigidbody.velocity = climbVelocity;
         myRigidbody.gravityScale = 0f;

         bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
         myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
       
    }

    void FlipSprite()
    {
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon ;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }
}
// This script handles player movement in a 2D platformer game.
// It includes running, jumping, and climbing mechanics.
// The player can move left and right, jump when on the ground, and climb ladders when touching climbing layers.
// The script uses Unity's Input System for handling player input.
// The player's sprite is flipped based on the direction of movement.
// The player's animation states are updated based on movement and climbing actions.
// The script also manages the player's Rigidbody2D and CapsuleCollider2D components for physics interactions.
// The gravity scale is adjusted when climbing to prevent falling while on a ladder.
// The script is designed to be attached to a player GameObject in a Unity scene.
// The player can run at a specified speed, jump with a specified force, and climb at a specified speed.
// The script uses serialized fields to allow customization of movement parameters in the Unity Inspector.
// The OnMove method captures player input for movement.
// The OnJump method handles jumping logic, ensuring the player can only jump when touching the ground.
// The climbLadder method manages climbing behavior, adjusting the Rigidbody2D's gravity scale and velocity.
// The Run method updates the player's horizontal velocity and animation state based on movement input.
// The FlipSprite method flips the player's sprite based on the direction of movement.
// The script is structured to be easily extendable for additional player movement features in the future.
// The script is optimized for performance by checking conditions before applying physics changes.
// The playerMovement script is a key component of the platformer game, providing essential movement mechanics for the player character.
// The script is written in C# and follows Unity's best practices for 2D game development.
