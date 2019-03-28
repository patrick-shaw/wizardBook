using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 5f;

    float gravityAtStart;

    Rigidbody2D heroRigidBody;
    CircleCollider2D heroFeet;
    Animator movementAnim;
    HeroHealth hero;


    // Use this for initialization
    void Start()
    {
        heroRigidBody = GetComponent<Rigidbody2D>();
        movementAnim = GetComponent<Animator>();
        heroFeet = GetComponent<CircleCollider2D>();
        hero = GetComponent<HeroHealth>();

        gravityAtStart = heroRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hero.isMobile)
        {
            return;
        }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        float playerInput = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(playerInput * runSpeed, heroRigidBody.velocity.y);
        heroRigidBody.velocity = playerVelocity;

        bool heroMovingX = Mathf.Abs(heroRigidBody.velocity.x) > Mathf.Epsilon;
        movementAnim.SetBool("Run", heroMovingX);
    }

    private void FlipSprite()
    {
        bool heroMovingX = Mathf.Abs(heroRigidBody.velocity.x) > Mathf.Epsilon;
        if (heroMovingX)
        {
            transform.localScale = new Vector2(Mathf.Sign(heroRigidBody.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        if (!heroFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jump = new Vector2(0f, jumpSpeed);
            heroRigidBody.velocity += jump;
            movementAnim.SetBool("Jump", true);
        }
        if (heroRigidBody.velocity.y <= 0)
        {
            movementAnim.SetBool("Jump", false);
        }
    }

    private void ClimbLadder()
    {
        if (!heroFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            movementAnim.SetBool("Climbing", false);
            heroRigidBody.gravityScale = gravityAtStart;
            return;
        }
        float playerInput = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(heroRigidBody.velocity.x, playerInput * climbSpeed);
        heroRigidBody.velocity = climbVelocity;

        heroRigidBody.gravityScale = 0f;
        if (heroRigidBody.gravityScale == 0 && CrossPlatformInputManager.GetAxis("Vertical") == 0) //Setting animation speed to zero when stop on ladder
        {
            movementAnim.speed = 0;
        }
        if (heroRigidBody.gravityScale == 0 && CrossPlatformInputManager.GetAxis("Vertical") != 0) //Setting animation speed to 1 while moving on ladder
        {
            movementAnim.speed = 1;
        }

        movementAnim.SetBool("Climbing", true);
    }

}
