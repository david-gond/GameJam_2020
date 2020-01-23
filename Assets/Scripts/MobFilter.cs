using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobFilter : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject[] canPass;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (GameObject gob in canPass)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), gob.GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
