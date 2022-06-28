using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController playerCC;
    [SerializeField] float speed = 10f;
    Vector3 move;
    private Vector3 velocity;
    [SerializeField] float gravity = -9.81f;
    float x;
    float z;
    public bool isGrounded;

    //for ground detection
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = .4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float jumpHeight=2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //ground check and smooth fall
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f; //smooth fall
        }
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;

        playerCC.Move(move*speed*Time.deltaTime);
        //gravity
        velocity.y+= gravity*Time.deltaTime;
        playerCC.Move(velocity*Time.deltaTime);//apply gravity to fall

        Jump();
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight*-2*gravity);
        }
    }
}
