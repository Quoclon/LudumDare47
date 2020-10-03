using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Anim_Controller : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<Skeleton_Controller>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleAnimation(float idleTimerCurrent, float moveTimerCurrent)
    {
        //Idle (NOT USED)
        if (idleTimerCurrent >= 0 && moveTimerCurrent <= 0)
        {
            anim.SetBool("walking", false);
        }

        //Moving
        if (moveTimerCurrent >= 0)
        {
            anim.SetBool("walking", true);
        }

        //Attacking
        if (idleTimerCurrent >= 0 && moveTimerCurrent <= 0)
        {
            //anim.SetBool("idle", true);
        }

    }
}
