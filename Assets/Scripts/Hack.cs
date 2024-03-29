﻿using System.Collections;
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
        if (character.HackableObject == null) return false;
        if (!base.Activate()) return false;
        var hackController = character.HackableObject.GetComponent<HackController>();
        hackController.Hack();
        return true;
    }
}
