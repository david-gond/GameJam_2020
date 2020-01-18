using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    private float last_activation;

    public float cooldown;

    public float RemainingTime
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        RemainingTime = 0;
        last_activation = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        float deltaTime = currentTime - last_activation;
        if (deltaTime > cooldown)
            RemainingTime = 0;
    }

    public virtual bool Activate()
    {
        Debug.Log("Remaining Time: " + RemainingTime);
        if (RemainingTime <= 0)
        {
            last_activation = Time.realtimeSinceStartup;
            RemainingTime = cooldown;
            return true;
        }
        return false;
    }
}
