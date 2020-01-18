using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Temp
    public int playerNumber;

    public int movespeed;

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

    private string horizontal;
    private string vertical;

    private float horizontalDirection;
    private float verticalDirection;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Shield = maxShield;
        Life = maxLife;
        SetPlayer(playerNumber);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Player"+playerNumber+"_Horizontal");
        verticalDirection = Input.GetAxis("Player"+playerNumber+"_Vertical");

        Move(horizontalDirection, verticalDirection);
    }

    private void Move(float hDirection, float vDirection)
    {
        Vector3 verticalVector = new Vector3(.0f, 1f, .0f);
        Vector3 horizontalVector = new Vector3(1f, .0f, .0f);
        if (vDirection != 0) // Moving up/down
        {
            this.transform.Translate(verticalVector * movespeed * Time.deltaTime);
            Debug.Log("Player " + playerNumber + " is moving " + (vDirection < 0 ? "down" : "up"));
        }
        if (hDirection != 0) // Moving left/right
        {
            this.transform.Translate(horizontalVector * movespeed * Time.deltaTime);
            Debug.Log("Player " + playerNumber + " is moving " + (hDirection < 0 ? "left" : "right"));
        }
    }
    public void SetPlayer(int number)
    {
        switch (number)
        {
            case 1:
                this.horizontal = "Player1";
                this.vertical = "Player1";
                break;
            case 2:
                this.horizontal = "Player2";
                this.vertical = "Player2";
                break;
            default:
                return;
        }
    }
}
