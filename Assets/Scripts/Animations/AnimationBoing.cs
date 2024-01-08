using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBoing : MonoBehaviour
{
    Animator animator;
    public bool rightPressed;
    public bool leftPressed;
    public bool jumpPressed;
    public bool isMovingRight;
    public bool isMovingLeft;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool jumpPressed = Input.GetKeyDown("w");


        if (rightPressed && !leftPressed)
        {
            isMovingRight = true;
        }
        else
        {
            isMovingRight = false;
        }
        if (leftPressed && !rightPressed)
        {
            isMovingLeft = true;
        }
        else
        {
            isMovingLeft = false;
        }
        if (jumpPressed)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
        animator.SetBool("RightPressed", isMovingRight);
        animator.SetBool("LeftPressed", isMovingLeft);
        animator.SetBool("JumpPressed", isJumping);
    }
}
