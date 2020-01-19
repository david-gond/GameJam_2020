using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /**
     * Properties
     */
    public int maxShield;
    [SerializeField]
    private int shield;
    public int Shield
    {
        get
        {
            return shield;
        }
        protected set
        {
            if (shield - value < 0)
                shield = value;
        }
    }

    public int maxLife;
    [SerializeField]
    private int life;
    public int Life
    {
        get
        {
            return life;
        }
        protected set
        {
            life = value;
            if (life <= 0)
                Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hackable":
                Infiltrator i = collision.gameObject.GetComponent<Character>() as Infiltrator;
                i.HackableObject = collision.gameObject;
                break;
        }
    }


    public bool Damage(int damage)
    {
        int damageLeft = damage - Shield;
        Shield -= damage;
        if (damageLeft > 0)
        {
            Life -= damageLeft;
        }
        return (Life > 0);
    }
}
