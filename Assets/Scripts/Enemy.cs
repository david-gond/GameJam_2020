using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : Character
{
    public float fieldOfView;
    public int movespeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Life = maxLife;
        Shield = maxShield;
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckVision();
    }

    private void CheckVision()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float angleToPlayer;
        Vector2 center_xy = new Vector2(transform.position.x, transform.position.y);
        Vector2 rightAxis = new Vector2(center_xy.x + 1, center_xy.y);
        Vector2 vector1;
        Vector2 vector2;
        Vector2 playerPosition;
        float angleForward = Vector2.SignedAngle(center_xy.normalized, transform.up);
        float distanceFromForward = 10 * Mathf.Tan(fieldOfView);
        Vector2 leftRay = 10f * transform.up + distanceFromForward * -transform.right;
        Vector2 rightRay = 10f * transform.up + distanceFromForward * transform.right;
        float leftAngle = Vector3.SignedAngle(transform.position, leftRay, Vector3.forward);
        float rightAngle = Vector3.SignedAngle(transform.position, rightRay, Vector3.forward);
        foreach (GameObject player in players)
        {
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            vector1 = center_xy - playerPosition;
            vector2 = center_xy - rightAxis;
            angleToPlayer = Vector2.SignedAngle(vector1.normalized, vector2.normalized);

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = LayerMask.GetMask("Player");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerPosition, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, playerPosition * 50, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, playerPosition * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
            if (angleToPlayer > leftAngle && angleToPlayer < rightAngle)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, movespeed * Time.deltaTime);
            }
        }
    }
}
