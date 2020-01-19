using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : Skill
{
    private Infiltrator character;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        character = GetComponent<Infiltrator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override bool Activate()
    {
        Debug.Log("Hack");
        if (character.HackableObject != null)
        {
            Debug.Log("Hackable");

            if (base.Activate())
            {
                Debug.Log("Hack complete");

                HackController hackController = character.HackableObject.GetComponent<HackController>();
                Destroy(hackController.hackable);
                return true;
            }
        }
        return false;
    }
}
