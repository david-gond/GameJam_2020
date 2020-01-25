using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGun : Skill
{
    public GameObject projectile;
    public AudioClip shotSound;

    private Player character;

    private GameObject gameManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.Find("GameManager");
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
        var audioSource = gameManager.GetComponent<AudioSource>();
        if (shotSound != null && audioSource != null)
        {
            Debug.Log(shotSound);
            audioSource.PlayOneShot(shotSound);
        }

        Instantiate(projectile, new Vector3(GetComponent<SpriteRenderer>().transform.position.x, transform.position.y, 0), Quaternion.AngleAxis(character.Orientation, Vector3.forward));
        return true;
    }
}
