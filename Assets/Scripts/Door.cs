using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : Hackable
{
    private Animator animator;
    private GameObject coll;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", false);
        coll = transform.Find("Collision").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void Open()
    {
        animator.SetBool("isOpen", true);
        // Manage collisions here
        coll.SetActive(false);
        Debug.Log("Open");
    }

    public override void Close()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("Close");
        coll.SetActive(true);
    }
}
