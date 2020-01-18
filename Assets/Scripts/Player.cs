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

    private float movementHorizontalDirection;
    private float movementVerticalDirection;

    private Rigidbody2D rb;

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
    }

    private void GetInput()
    {
        movementHorizontalDirection = Input.GetAxis("Player" + playerNumber + "_Horizontal");
        movementVerticalDirection = Input.GetAxis("Player" + playerNumber + "_Vertical");
    }

    private void CalculateDirection()
    {

    }
    private void Rotate()
    {

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
