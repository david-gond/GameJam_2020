using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Character
{
    public enum OrientationMode
    {
        Joystick,
        Keyboard
    }

    private const int east = 0;
    private const int northEast = 45;
    private const int north = 90;
    private const int northWest = 135;
    private const int west = 180;
    private const int southWest = 225;
    private const int south = 270;
    private const int southEast = 315;


    /**
     * Fields
     */
    private int playerNumber;

    private float angle;
    private Vector2 currentMousePosition;
    private float movementHorizontalDirection;
    private float movementVerticalDirection;
    private float main_action;
    private float secondary_action;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isPrimaryAxisInUse = false;
    private bool isSecondaryAxisInUse = false;

    public int movespeed;
    public OrientationMode orientationMode;
    public Skill primarySkill;

    public Skill secondarySkill;

    public float Orientation
    {
        get => angle;
        protected set { angle = value % 360; }
    }

    // Start is called before the first frame update
    protected new virtual void Start()
    {
        base.Start();
        Shield = maxShield;
        Life = maxLife;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    protected new virtual void Update()
    {
        GetInput();

        CalculateDirection();
        Rotate();
    }

    protected virtual void FixedUpdate()
    {
        Move(movementHorizontalDirection, movementVerticalDirection);
    }

    private void GetInput()
    {
        movementHorizontalDirection = Input.GetAxis("Player" + playerNumber + "_Horizontal");
        movementVerticalDirection = Input.GetAxis("Player" + playerNumber + "_Vertical");

        string primaryString = "Player" + playerNumber + "_Primary";
        if (Input.GetAxisRaw(primaryString) != 0)
        {
            if (isPrimaryAxisInUse == false)
            {
                Primary();
                isPrimaryAxisInUse = true;
            }
        }

        if (Input.GetAxisRaw(primaryString) == 0)
        {
            isPrimaryAxisInUse = false;
        }

        string secondaryString = "Player" + playerNumber + "_Secondary";
        if (Input.GetAxisRaw(secondaryString) != 0)
        {
            if (isSecondaryAxisInUse == false)
            {
                Secondary();
                isSecondaryAxisInUse = true;
            }
        }

        if (Input.GetAxisRaw(secondaryString) == 0)
        {
            isSecondaryAxisInUse = false;
        }
    }

    private void CalculateDirection()
    {
        var mousePos_xy = new Vector2(0, 0);
        switch (orientationMode)
        {
            case OrientationMode.Joystick:
                string verticalAxis = "Player" + playerNumber + "_RStick_Horizontal";
                string horizontalAxis = "Player" + playerNumber + "_RStick_Vertical";
                float xAxis = Input.GetAxis(horizontalAxis);
                float yAxis = Input.GetAxis(verticalAxis);

                angle = Mathf.Atan2(Input.GetAxis(verticalAxis), Input.GetAxis(horizontalAxis)) * 180 / Mathf.PI;
                break;
            case OrientationMode.Keyboard:
                if (movementHorizontalDirection == 0 && movementVerticalDirection == 0)
                {
                    animator.SetBool("isMoving", false);
                    return;
                }

                animator.SetBool("isMoving", true);
                if (movementHorizontalDirection > 0)
                {
                    if (movementVerticalDirection > 0) // North East
                        Orientation = northEast;
                    else if (movementVerticalDirection < 0) // South East
                        Orientation = southEast;
                    else // East
                        Orientation = east;
                }
                else if (movementHorizontalDirection < 0)
                {
                    if (movementVerticalDirection > 0) // North West
                        Orientation = northWest;
                    else if (movementVerticalDirection < 0) // South West
                        Orientation = southWest;
                    else // West
                        Orientation = west;
                }
                else if (movementHorizontalDirection == 0)
                {
                    if (movementVerticalDirection > 0) // North
                        Orientation = north;
                    else if (movementVerticalDirection < 0) // South
                        Orientation = south;
                }

                break;
            default:
                return;
        }
    }

    private void Rotate()
    {
        if ((east + northEast) / 2f <= angle && angle < (northEast + north) / 2f) // North East
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", 1);
        }
        else if ((northEast + north) / 2f <= angle && angle < (north + northWest) / 2f) // North
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", 0);
        }
        else if ((north + northWest) / 2f <= angle && angle < (northWest + west) / 2f) // North West
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", -1);
        }
        else if ((northWest + west) / 2f <= angle && angle < (west + southWest) / 2f) // West
        {
            animator.SetInteger("facingUp", 0);
            animator.SetInteger("facingRight", -1);
        }
        else if ((west + southWest) / 2f <= angle && angle < (southWest + south) / 2f) // South West
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", -1);
        }
        else if ((southWest + south) / 2f <= angle && angle < (south + southEast) / 2f) // South
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", 0);
        }
        else if ((south + southEast) / 2f <= angle && angle < (southEast + east) / 2f) // South east
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", 1);
        }
        else // East
        {
            animator.SetInteger("facingUp", 0);
            animator.SetInteger("facingRight", 1);
        }
    }

    public void Move(float hDirection, float vDirection)
    {
        if ((hDirection == 0f) && (vDirection == 0f))
        {
            animator.SetBool("isMoving",false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        var moveVector = new Vector3(hDirection, vDirection, .0f);
        rb.MovePosition(new Vector2(transform.position.x + moveVector.x * movespeed * Time.deltaTime,
            transform.position.y + moveVector.y * movespeed * Time.deltaTime));
    }

    public void SetPlayerControls(int number)
    {
        if (number < 1 || number > 2)
        {
            return;
        }

        playerNumber = number;
    }

    protected void Primary()
    {
        ActivateSkill(primarySkill);
    }

    protected void Secondary()
    {
        ActivateSkill(secondarySkill);
    }

    private static void ActivateSkill(Skill skill)
    {
        if (skill != null)
            skill.Activate();
    }
}