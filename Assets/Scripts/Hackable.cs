using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hackable : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public abstract void Open();

    public abstract void Close();
}
