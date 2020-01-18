using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D rigid;

    public int damage;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.AddForce(0.05f * -transform.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gob = collision.gameObject;

        switch (gob.tag)
        {
            
            default:
                Destroy(this.gameObject);
                break;
        }
        /*
        if (gob.tag == "Enemy")
        {
            gob.GetComponent<Enemy>().Hit(damage);
        }
        if (gob.tag == "Boss")
        {
            gob.GetComponent<Boss>().Hit(damage);
        }*/

        
    }
}
