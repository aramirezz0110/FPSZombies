using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayerMovement : MonoBehaviour
{
    CharacterController playerCC;
    [SerializeField] float speed = 10f;
    Vector3 move;
    private Vector3 velocity;
    [SerializeField] float gravity = -9.81f;
    float x;
    float z;
    public bool isGrounded;

    //for touch detection
    private int leftFingerID;
    private int rightFingerID;
    private float halfScreen;

    public Transform playerCamera;

    private Vector2 moveInput;
    private Vector2 moveTouchStartPosition;

    private Vector2 lookInput;
    [SerializeField] private float cameraSensibility;
    private float cameraPitch;

    //for ground detection
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = .4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float jumpHeight=2.0f;
    // Start is called before the first frame update
    void Start()
    {
        leftFingerID = -1;
        rightFingerID = -1;
        halfScreen = Screen.width / 2f; //calculate the mid of screen
        playerCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //touch
        GetTouchInput();

        if (leftFingerID != -1)
        {
            Move();
        }
        if(rightFingerID != -1)
        {
            LookAround();
        }

        Jump();

        
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight*-2*gravity);
        }
    }
    private void GetTouchInput()
    {
        Touch t;
        for(int i=0; i<Input.touchCount; i++)
        {
            t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x < halfScreen && leftFingerID == -1)
                {
                    leftFingerID = t.fingerId;
                    moveTouchStartPosition = t.position;
                }
                else if (t.position.x > halfScreen && rightFingerID == -1)
                {
                    rightFingerID = t.fingerId;
                }
            }
            if(t.phase == TouchPhase.Canceled)
            {

            }
            if(t.phase == TouchPhase.Moved)
            {
                if(leftFingerID == t.fingerId)
                {
                    moveInput = t.position - moveTouchStartPosition;
                }else if(rightFingerID == t.fingerId)
                {
                    lookInput = t.deltaPosition * cameraSensibility * Time.deltaTime;
                }
            }
            if(t.phase == TouchPhase.Stationary)
            {
                if(rightFingerID == t.fingerId)
                {
                    lookInput = Vector2.zero;
                }
            }
            if(t.phase == TouchPhase.Ended)
            {
                if(leftFingerID == t.fingerId)
                {
                    leftFingerID = -1;
                } else if (rightFingerID == t.fingerId)
                {
                    rightFingerID = -1;
                }
            }
        }
    } 
    private void Move()
    {
        //check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //ground check and smooth fall
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //smooth fall
        }
        x = moveInput.normalized.x;
        z = moveInput.normalized.y;
        move = transform.right * x + transform.forward * z;

        playerCC.Move(move * speed * Time.deltaTime);
        //gravity
        velocity.y += gravity * Time.deltaTime;
        playerCC.Move(velocity * Time.deltaTime);//apply gravity to fall
    }
    private void LookAround()
    {
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        transform.Rotate(Vector3.up, lookInput.x);
    }
}
