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
        bool rightPressed = Input.GetKeyDown("d");
        bool leftPressed = Input.GetKeyDown("a");
        bool jumpPressed = Input.GetKey("w");

        if (rightPressed)
        {
            isMovingRight = true;
        }
        if (leftPressed)
        {
            isMovingLeft = true;
        }
        animator.SetBool("RightPressed", isMovingRight);
        animator.SetBool("LeftPressed", isMovingLeft);
    }
}
