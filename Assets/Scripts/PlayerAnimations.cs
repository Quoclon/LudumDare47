using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    AudioManager audioManager;

    private Vector2 moveInput;
    public Animator anim;
    public Stats stats;


    // Start is called before the first frame update
    void Start()
    {
        //Get the Global Audio Manager to Use for Audio
        audioManager = FindObjectOfType<AudioManager>();
        stats = GetComponentInParent<Stats>();

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = GameObject.Find("Player").GetComponent<PlayerController>().moveInput;

        // Check the transition from Idle to Running
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
            audioManager.playAudio("Running");
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // Check if the Attack key was pressed.
        if (Input.GetMouseButton(0))
        {
            // Check if the Player is in an roll animation 
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll"))
            {
                audioManager.playAudio("RollAttack");
                // Allow the Player to do a rolling attack
                anim.SetBool("canRollAttack", true);
            }
            else
            {
                audioManager.playAudio("SlashBasic");
                // Allow the Player to do a slash attack
                anim.SetBool("swordSlice", true);
            }
        }

        // Turn off Roll Attack
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll_attack"))
        {
            anim.SetBool("canRollAttack", false);
        }

        // Turn off Slash Attack
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_attack"))
        {
            anim.SetBool("swordSlice", false);
        }


        // Check if the Roll key is being pressed. If so set the isRolling bool to true
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("isRolling", true);
            //audioManager.playAudio("Roll");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isRolling", false);
        }

        // While the rolling animation is playing, set moveSpeedCurrent to moveSpeedRolling
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll"))
        {
            stats.UpdateMoveSpeed(stats.moveSpeedRolling);
        }
        else
        {
            stats.UpdateMoveSpeed(stats.moveSpeedMax);
        }

        //Sword Slash Collison
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_attack") && stats.hitEnemy == false)
        {
            GameObject.Find("Sword Slash Collider").GetComponent<CircleCollider2D>().enabled = true;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_attack"))
        {
            GameObject.Find("Sword Slash Collider").GetComponent<CircleCollider2D>().enabled = false;
            stats.hitEnemy = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll_attack") && stats.hitEnemy == false)
        {
            GameObject.Find("Roll Attack Collider").GetComponent<CircleCollider2D>().enabled = true;
        }
        else
        {
            GameObject.Find("Roll Attack Collider").GetComponent<CircleCollider2D>().enabled = false;
            stats.hitEnemy = false;
        }

        // Player Death Animation
        if(stats.health <= 0 && stats.deathState == false)
        {
            stats.deathState = true;
            anim.SetTrigger("isDying");
            audioManager.playAudio("PlayerDead");
        }
    }

}
