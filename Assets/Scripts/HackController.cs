using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour
{
    public GameObject hackable;

    private bool isHackable;
    // Start is called before the first frame update
    void Start()
    {
        isHackable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hack available");
            isHackable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hack not available");
            isHackable = false;
        }
    }
}
