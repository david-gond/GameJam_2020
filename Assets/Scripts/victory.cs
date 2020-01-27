using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory : MonoBehaviour
{
    public GameObject endscreen;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().isScreenSplit = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().canSplitScreen = false;
            endscreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
