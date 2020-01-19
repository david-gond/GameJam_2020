using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject origin;
    public string team;

    public int damage;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(0.25f * -transform.right);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), origin.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigid.AddForce(0.025f * -transform.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gob = collision.gameObject;
        Character character = gob.GetComponent<Character>();
        Debug.Log(team);
        if (character != null)
        {
            switch (team)
            {
                case "players":
                    if (gob.tag.Contains("Enemy"))
                        character.Damage(1);
                    break;
                case "enemies":
                    if (gob.tag.Contains("Player"))
                        character.Damage(1);
                    break;
            }
        }
        Destroy(gameObject);
    }
}
