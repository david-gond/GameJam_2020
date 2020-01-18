using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public enum OrientationMode { Mouse, Joystick }

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

    public int movespeed;
    public OrientationMode orientationMode;
    public Skill PrimarySkill;
   
    public Skill SecondarySkill
    {
        get;
        protected set;
    }

    /**
     * Properties
     */
    public int maxShield = 100;
    [SerializeField]
    private int shield;
    public int Shield
    {
        get
        {
            return this.shield;
        }
        private set
        {
            if (this.shield - value < 0)
                this.shield = value;
        }
    }

    public int maxLife = 1;
    [SerializeField]
    private int life;
    public int Life
    {
        get
        {
            return this.life;
        }
        private set
        {
            if (this.life - value < 0)
                this.life = value;
        }
    }

    public float Orientation
    {
        get
        {
            return angle;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Shield = maxShield;
        Life = maxLife;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
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

        main_action = Input.GetAxis("Player" + playerNumber + "_Primary");
        secondary_action = Input.GetAxis("Player" + playerNumber + "_Secondary");

        if (main_action == 1)
            Primary();
        else if (secondary_action == 1)
            Secondary();
    }

    private void CalculateDirection()
    {
        Vector2 mousePos_xy = new Vector2(0, 0);
        switch (orientationMode)
        {
            case OrientationMode.Mouse:
                mousePos_xy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 center_xy = new Vector2(this.transform.position.x, this.transform.position.y);
                var vector1 = center_xy - mousePos_xy; // VectorToMoveTo
                var vector2 = center_xy - new Vector2(center_xy.x+1, center_xy.y); // Vector right at the right of player

                angle = Vector2.SignedAngle(vector1.normalized, vector2.normalized);
                break;
            case OrientationMode.Joystick:
                string verticalAxis = "Player" + playerNumber + "_LStick_Horizontal";
                string horizontalAxis = "Player" + playerNumber + "_LStick_Vertical";
                float xAxis = Input.GetAxis(horizontalAxis);
                float yAxis = Input.GetAxis(verticalAxis);

                angle = Mathf.Atan2(Input.GetAxis(verticalAxis), Input.GetAxis(horizontalAxis)) * 180 / Mathf.PI;
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

        string direction = "";
        if (angle <= east && angle > northEast)
            direction = "E";
        if (angle <= northEast && angle > north)
            direction = "NE";
        if (angle <= north && angle > northWest)
            direction = "N";
        if (angle <= northWest && angle > west)
            direction = "NW";
        if (angle <= west || angle > southWest)
            direction = "W";
        if (angle <= southWest && angle > south)
            direction = "SW";
        if (angle <= south && angle > southEast)
            direction = "S";
        if (angle <= southEast && angle > east)
            direction = "SE";
        
    }
    public void Move(float hDirection, float vDirection)
    {
        Vector3 moveVector = new Vector3(hDirection, vDirection, .0f);
        rb.MovePosition(new Vector2(transform.position.x + moveVector.x * movespeed * Time.deltaTime,
            transform.position.y + moveVector.y * movespeed * Time.deltaTime));
    }
    public void SetPlayerControls(int number)
    {
        if (number < 1 || number > 2)
        {
            return;
        }
        this.playerNumber = number;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }

    protected void Primary()
    {
        PrimarySkill.Activate();
    }

    protected void Secondary()
    {
        SecondarySkill.Activate();
    }
}
