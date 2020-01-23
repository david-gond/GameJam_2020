using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGun : Skill
{
    public GameObject projectile;
    private Player character;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        character = GetComponent<Player>();
        var pc = projectile.GetComponent<ProjectileController>();
        pc.origin = character.gameObject;
        pc.team = "players";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;
        Instantiate(projectile, new Vector3(GetComponent<SpriteRenderer>().transform.position.x, transform.position.y, 0), Quaternion.AngleAxis(character.Orientation, Vector3.forward));
        return true;
    }
}
