using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infiltrator : Player
{
    public GameObject HackableObject
    {
        get;
        set;
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
