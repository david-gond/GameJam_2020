using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Character
{
    public enum OrientationMode
    {
        Mouse,
        Joystick,
        Keyboard
    }

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
        protected set
        {
            if (value < -180)
            {
                value = -180;
            }

            if (value > 180)
            {
                value = 180;
            }

            angle = value;
        }
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
            case OrientationMode.Mouse:
                mousePos_xy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 center_xy = new Vector2(transform.position.x, transform.position.y);
                var vector1 = center_xy - mousePos_xy; // VectorToMoveTo
                var vector2 =
                    center_xy - new Vector2(center_xy.x + 1, center_xy.y); // Vector right at the right of player
                angle = Vector2.SignedAngle(vector1.normalized, vector2.normalized);
                break;
            case OrientationMode.Joystick:
                string verticalAxis = "Player" + playerNumber + "_RStick_Horizontal";
                string horizontalAxis = "Player" + playerNumber + "_RStick_Vertical";
                float xAxis = Input.GetAxis(horizontalAxis);
                float yAxis = Input.GetAxis(verticalAxis);

                angle = Mathf.Atan2(Input.GetAxis(verticalAxis), Input.GetAxis(horizontalAxis)) * 180 / Mathf.PI;
                break;
            case OrientationMode.Keyboard:
                if (movementHorizontalDirection == 0 && movementVerticalDirection == 0)
                    return;
                if (movementHorizontalDirection > 0)
                {
                    if (movementVerticalDirection > 0) // North East
                        angle = 45;
                    else if (movementVerticalDirection < 0) // South East
                        angle = 315;
                    else // East
                        angle = 0;
                }
                else if (movementHorizontalDirection < 0)
                {
                    if (movementVerticalDirection > 0) // North West
                        angle = 135;
                    else if (movementVerticalDirection < 0) // South West
                        angle = 225;
                    else // West
                        angle = 180;
                }
                else if (movementHorizontalDirection == 0)
                {
                    if (movementVerticalDirection > 0) // North
                        angle = 90;
                    else if (movementVerticalDirection < 0) // South
                        angle = 270;
                }
                break;
            default:
                return;
        }
    }

    private void Rotate()
    {
        int northEast = -23;
        int north = -68;
        int northWest = -113;
        int west = -158;
        int southWest = 158;
        int south = 113;
        int southEast = 68;
        int east = 23;

        if (angle <= east && angle > northEast) // East
        {
            animator.SetInteger("facingUp", 0);
            animator.SetInteger("facingRight", 1);
        }

        if (angle <= northEast && angle > north) // North East
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", 1);
        }

        if (angle <= north && angle > northWest) // North
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", 0);
        }

        if (angle <= northWest && angle > west) // North West
        {
            animator.SetInteger("facingUp", 1);
            animator.SetInteger("facingRight", -1);
        }

        if (angle <= west || angle > southWest) // West
        {
            animator.SetInteger("facingUp", 0);
            animator.SetInteger("facingRight", -1);
        }

        if (angle <= southWest && angle > south) // South West
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", -1);
        }

        if (angle <= south && angle > southEast) // South
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", 0);
        }

        if (angle <= southEast && angle > east) // South east
        {
            animator.SetInteger("facingUp", -1);
            animator.SetInteger("facingRight", 1);
        }
    }

    public void Move(float hDirection, float vDirection)
    {
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