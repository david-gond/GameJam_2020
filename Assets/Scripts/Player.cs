using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    private Rigidbody2D rb;

    public int movespeed;
    public OrientationMode orientationMode;


    /**
     * Properties
     */
    public int maxShield;
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

    public int maxLife;
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

    // Start is called before the first frame update
    void Start()
    {
        Shield = maxShield;
        Life = maxLife;
        SetPlayerControls(playerNumber);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        CalculateDirection();
        Rotate();
        Move(movementHorizontalDirection, movementVerticalDirection);
        Debug.Log("Player"+playerNumber+":"+ angle);
    }

    private void GetInput()
    {
        movementHorizontalDirection = Input.GetAxis("Player" + playerNumber + "_Horizontal");
        movementVerticalDirection = Input.GetAxis("Player" + playerNumber + "_Vertical");
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
                
                break;
            default:
                return;
        }
        //float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        //transform.Rotate(0, angle, 0);
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
        Debug.Log("Player" + playerNumber + " of angle " + angle + " is looking " + direction);
    }
    private void Move(float hDirection, float vDirection)
    {
        Vector3 verticalVector = new Vector3(.0f, 1f, .0f);
        Vector3 horizontalVector = new Vector3(1f, .0f, .0f);
        if (vDirection != 0) // Moving up/down
        {
            this.transform.Translate(verticalVector * vDirection * movespeed * Time.deltaTime);
            Debug.Log("Player " + playerNumber + " is moving " + (vDirection < 0 ? "down" : "up"));
        }
        if (hDirection != 0) // Moving left/right
        {
            this.transform.Translate(horizontalVector * hDirection * movespeed * Time.deltaTime);
            Debug.Log("Player " + playerNumber + " is moving " + (hDirection < 0 ? "left" : "right"));
        }
    }
    public void SetPlayerControls(int number)
    {
        if (number < 1 || number > 2)
        {
            Debug.Log("Error when trying to assign number player: " + number);
            return;
        }
        this.playerNumber = number;
    }
}
