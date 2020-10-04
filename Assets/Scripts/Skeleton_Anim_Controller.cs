using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Anim_Controller : MonoBehaviour
{
    Animator anim;
    Skeleton_Controller skeletonController;
    Stats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<Skeleton_Controller>();
        anim = GetComponent<Animator>();
        skeletonController = GetComponentInParent<Skeleton_Controller>();
        stats = GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleAnimation()
    {
        float idleTimerCurrent = skeletonController.idleTimerCurrent;
        float moveTimerCurrent = skeletonController.moveTimerCurrent;
        bool attacking = skeletonController.attacking;


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
        if (stats.health > 100)
        {
            anim.SetBool("attacking", true);
        }

        if (stats.deathState)
        {
            //anim.SetBool("dying", true);
        }

    }
}
