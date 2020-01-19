using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infiltrator : Character
{
    public GameObject HackableObject
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Hackable":
                HackableObject = collision.gameObject;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hackable":
                HackableObject = null;
                break;
        }
    }
}
