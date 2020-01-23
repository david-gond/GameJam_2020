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
        get => shield;
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
        get => life;
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
    
    public bool Damage(int damage)
    {
        var damageLeft = damage - Shield;
        Shield -= damage;
        if (damageLeft > 0)
        {
            Life -= damageLeft;
        }
        return (Life > 0);
    }
}
