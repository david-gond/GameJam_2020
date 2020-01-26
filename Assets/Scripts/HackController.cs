using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour
{
    public Hackable[] hackable;
    public AudioClip hack;

    private AudioSource audio;
    private bool isHackable;

    // Start is called before the first frame update
    private void Start()
    {
        isHackable = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isHackable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isHackable = false;
        }
    }

    public void Hack()
    {
        foreach (var hack in hackable)
        {
            hack.Open();
        }
        audio.PlayOneShot(hack);
    }
}