using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject origin;
    public string team;

    public int damage;

    // Use this for initialization
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(rigid.mass * 2500f * transform.right);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), origin.GetComponent<Collider2D>());
    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var gob = collision.gameObject;
        var character = gob.GetComponent<Character>();
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
