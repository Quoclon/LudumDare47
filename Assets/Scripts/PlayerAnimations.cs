using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    AudioManager audioManager;

    public float moveSpeed;
    public float moveSpeedCurrent;
    public float moveSpeedRolling;
    private Vector2 moveInput;
    public Animator anim;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //Get the Global Audio Manager to Use for Audio
        audioManager = FindObjectOfType<AudioManager>();


        moveSpeed = GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed;
        moveSpeedCurrent = moveSpeed;
        moveSpeedRolling = moveSpeed * 2f;
        health = GameObject.Find("Player").GetComponent<PlayerController>().health;
        Debug.Log(health);

    }

    // Update is called once per frame
    void Update()
    {
        //Set the Global Audio Manager


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

        // Check if the Attack key was pressed.
        if (Input.GetMouseButton(0))
        {
            // Check if the Player is in an roll animation 
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll"))
            {
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
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll_attack"))
        {
            // Enable the Collider
            GameObject.Find("Roll Attack Collider").GetComponent<CircleCollider2D>().enabled = true;
            Debug.Log("Roll Atack HIT");

        }
        else
        {
            GameObject.Find("Roll Attack Collider").GetComponent<CircleCollider2D>().enabled = false;
        }

    }

}
