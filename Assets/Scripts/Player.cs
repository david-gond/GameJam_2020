using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxShield;
    [SerializeField]
    private int shield;
    public int Shield
    {
        get
        {
            return this.shield;
        }
        private set
        {
            if (this.shield - value < 0)
                this.shield = value;
        }
    }

    public int maxLife;
    [SerializeField]
    private int life;
    public int Life
    {
        get
        {
            return this.life;
        }
        private set
        {
            if (this.life - value < 0)
                this.life = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Shield = maxShield;
        Life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
