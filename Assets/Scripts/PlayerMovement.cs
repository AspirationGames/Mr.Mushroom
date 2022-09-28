using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 jumpInput;
    Rigidbody2D playerRigidbody;
    Transform playerTransform;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    Animator playerAnimator;

    [SerializeField] Transform playerBow;
    [SerializeField] GameObject arrow;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;

    [SerializeField] float climbSpeed = 5f;

    [SerializeField] float deathSpeed = 10f;

    [SerializeField] float arrowDelay = 1f;


    bool playerHasHorizontalMovement;
    bool isAlive;
   public bool hasBow;

    float playerGravity;

    private void Awake() 
    {
     playerRigidbody = GetComponent<Rigidbody2D>();
     playerTransform = GetComponent<Transform>();
     playerAnimator = GetComponent<Animator>(); 
     playerCollider = GetComponent<CapsuleCollider2D>();
     playerFeetCollider = GetComponent<BoxCollider2D>();
     
    }
    
    void Start()
    {
        playerGravity = playerRigidbody.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        if(!isAlive) {return;}
        Run();
        FlipSprite();
        Climbing();
        Die();

    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        //Debug.Log("Your move input is " + moveInput);
        
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}
        bool isTouchingGround = playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
        if(isTouchingGround)
        {
            if(value.isPressed)
            {
                playerRigidbody.velocity += new Vector2 (0f, jumpSpeed);
            }
        }
        
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}

        playerAnimator.SetTrigger("Shoot");
        StartCoroutine("SpawnArrow");
        
        

    }

    IEnumerator SpawnArrow()
    {
        yield return new WaitForSecondsRealtime(arrowDelay);
        Instantiate(arrow, playerBow.position, transform.rotation);
        //Instantiate Spawns the Arrow our arrow velocity comes from the Arrow script

    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        
        playerHasHorizontalMovement = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalMovement);
        

    }

    void FlipSprite()
    {
        playerHasHorizontalMovement = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalMovement)
        {
            playerTransform.localScale = new Vector2 (Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
        
    }

    void Climbing()
    {
        bool isTouchingClimbing = playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        
        bool playerHasVerticalVelocity = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;

        playerAnimator.SetBool("isClimbing",playerHasVerticalVelocity);

        if(!isTouchingClimbing)
        {
            playerRigidbody.gravityScale = playerGravity;
            playerAnimator.SetBool("isClimbing",isTouchingClimbing);
            return;
            
        }

        Vector2 climbVelocity = new Vector2 (playerRigidbody.velocity.x, moveInput.y*climbSpeed);
        playerRigidbody.velocity = climbVelocity;
        playerRigidbody.gravityScale = 0f;
        

    }

    void Die()
    {
        bool isTouchingEnemy = playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"));
        
        if(isTouchingEnemy)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidbody.velocity += new Vector2 (deathSpeed *Mathf.Sign(playerRigidbody.velocity.x), deathSpeed);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }



}
