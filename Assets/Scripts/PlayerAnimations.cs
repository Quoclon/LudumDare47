using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    
    public float moveSpeed;
    public float moveSpeedCurrent;
    public float moveSpeedRolling;
    private Vector2 moveInput;
    public Animator anim;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed;
        moveSpeedCurrent = moveSpeed;
        moveSpeedRolling = moveSpeed * 2f;
        health = GameObject.Find("Player").GetComponent<PlayerController>().health;
        Debug.Log(health);

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = GameObject.Find("Player").GetComponent<PlayerController>().moveInput;
        //health = GameObject.Find("Player").GetComponent<PlayerController>().health;

        //-----ANIMATOR-----//

        // Check the transition from Idle to Running
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // Check if the Attack key is being pressed. If so set the swordSlice bool to true
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("swordSlice2");

            /*
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll"))
            {
                anim.SetTrigger("rollAttack");
            }
            else
            {
                anim.SetBool("swordSlice", true);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
     
            anim.SetBool("swordSlice", false);
            anim.SetBool("isRollAttacking", false);
            */
        }

            // Check if the Roll key is being pressed. If so set the isRolling bool to true
            if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("isRolling", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isRolling", false);
        }

        // While the rolling animation is playing, set moveSpeedCurrent to moveSpeedRollingbe
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().setMoveSpeedCurrent(moveSpeedRolling);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerController>().setMoveSpeedCurrent(moveSpeed);
        }

        // Player Dead Animation: Check if Player's health <= 0
        if(health <= 0)
        {
            anim.SetBool("isPlayerDead", true);
            Debug.Log("Player died");
        }

        // Damage Player Health
        if(Input.GetMouseButtonDown(2))
        {
            health -= 50;
            Debug.Log(health);
        }


        //*****BOX COLIDERS****//

        //Sword Slash
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_attack"))
        {
            // Enable the Collider
            GameObject.Find("Sword Slash Collider").GetComponent<CircleCollider2D>().enabled = true;
            
        }
        else
        {
            GameObject.Find("Sword Slash Collider").GetComponent<CircleCollider2D>().enabled = false;
            //Debug.Log("Sword OFF");
        }

    }

}
