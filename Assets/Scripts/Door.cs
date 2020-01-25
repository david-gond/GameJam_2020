using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : Hackable
{
    private Animator animator;
    private GameObject coll;
    private AudioSource audio;

    public AudioClip open;
    public AudioClip close;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", false);
        coll = transform.Find("Collision").gameObject;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void Open()
    {
        animator.SetBool("isOpen", true);
        coll.SetActive(false);
        audio.PlayOneShot(open);
    }

    public override void Close()
    {
        animator.SetBool("isOpen", false);
        coll.SetActive(true);
        audio.PlayOneShot(close);

    }
}
