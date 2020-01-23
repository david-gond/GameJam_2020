using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Infiltrator : Player
{
    public GameObject HackableObject { get; set; }

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

    private void OnTriggerEnter2D(Component collision)
    {
        var gob = collision.gameObject;
        switch (collision.gameObject.tag)
        {
            case "Hackable":
                HackableObject = gob;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var gob = collision.gameObject;
        switch (gob.tag)
        {
            case "Hackable":
                HackableObject = null;
                break;
        }
    }
}