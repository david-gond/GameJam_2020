using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int nbMinutes = 2;
    private int timer;
    public Text affich_time;
    private int minuts;
    private int seconds;
    private int previous_up;

    // Start is called before the first frame update
    void Start()
    {
        timer = nbMinutes*60;
        TimeChange();
        previous_up = 0;
    }

    private void FixedUpdate()
    {
        if (previous_up >= 50)
        {
            TimeChange();
            previous_up = 0;
        }
        else
            previous_up++;
    }

    private void TimeChange()
    {
        timer--;
        minuts = timer / 60;
        seconds = timer - (minuts * 60);
        affich_time.text = (minuts.ToString()+":"+seconds.ToString());
    }
}
