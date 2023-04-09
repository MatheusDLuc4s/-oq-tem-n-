using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float dashSpeed;

    public float groundDrag;
        
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    private Animator animator;

    public enum MovementState
    {
        freeze,
        walking,
        dashing,
        jumping
    }

    public bool dashing;
    public bool jumping;
    public bool freeze;
    private bool dash;
    private bool jump;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        dash = false;
        animator = GetComponent<Animator>();
    } 

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f , whatisGround);
        animator.SetFloat("mSpeed", rb.velocity.magnitude);
        animator.SetBool("Impulso", dash);
        animator.SetBool("Pulo", jump);
        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag
        if(state == MovementState.walking)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void StateHandler()
    {
        //teleport
        if (freeze)
        {
            state = MovementState.freeze;
            moveSpeed = 0;
            rb.velocity = Vector3.zero;
            dash = false;
            jump = false;
        }
        // mode - dashing
        else if(dashing)
        {
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
            dash = true;
            jump = false;
        }

        else if(jumping)
        {
            state = MovementState.jumping;
            moveSpeed = walkSpeed;
            dash = false;
            jump = true;
        }

        else 
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            dash = false;
            jump = false;
        }
    }

    private void movePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);       
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

}