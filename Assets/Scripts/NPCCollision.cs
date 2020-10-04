using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    public float attackDelay;
    public bool axe;
    public bool fireBall;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(axe)
            {
                if (GetComponentInParent<Stats>().hitEnemy == false)
                {
                    //if(other.GetComponentInChildren<Animator>().GetBool)
                    other.GetComponent<Stats>().DamageHealth(GetComponentInParent<Stats>().dmgMax);
                    GetComponentInParent<Stats>().hitEnemy = true;
                    StartCoroutine(resetHit());
                }
            }
            if (fireBall)
            {
                if (GetComponentInParent<Stats>().hitEnemy == false)
                {
                    other.GetComponent<Stats>().DamageHealth(GetComponentInParent<Stats>().dmgMax);
                    GetComponentInParent<Stats>().hitEnemy = true;
                    StartCoroutine(resetHit());
                }
            }
            else
            {
                if (GetComponent<Stats>().hitEnemy == false)
                {
                    /*other.GetComponent<Stats>().DamageHealth(GetComponent<Stats>().dmg);
                    GetComponent<Stats>().hitEnemy = true;
                    StartCoroutine(resetHit());*/
                }
                    
            }

        }
    }

    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(attackDelay);
        if(axe)
        {
            GetComponentInParent<Stats>().hitEnemy = false;
        }
        else
        {
            GetComponentInParent<Stats>().hitEnemy = false;
        }
        
    }

}
