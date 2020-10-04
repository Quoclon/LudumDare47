using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    Skeleton_Anim_Controller anim_controller;
    GameObject player;

    [SerializeField] private float moveTimer;
    private float moveTimerMin = 1.0f;
    private float moveTimerMax = 6.0f;
    public float moveTimerCurrent;

    [SerializeField] private float idleTimer;
    private float idleTimerMin = 0.25f;
    private float idleTimerMax = 3.0f;
    public float idleTimerCurrent;

    [SerializeField]
    private float attackTimer = 3.0f;
    //private float attackTimerMin = 0.25f;
    //private float attackTimerMax = 3.0f;
    public float attackTimerCurrent;

    private float moveSpeed = 0.5f;
    private float randMoveX;
    private float randMoveY;

    public bool attacking = false;
    public float attackRange;
    
    public float aggroRange;
    public bool chasing;
    public Vector3 chaseDirection;





    // Start is called before the first frame update
    void Start()
    {
        anim_controller = GetComponentInChildren<Skeleton_Anim_Controller>();

        player = GameObject.FindWithTag("Player");

        chasing = false;

        //Set the move timer
        moveTimer = UnityEngine.Random.Range(moveTimerMin, moveTimerMax+1);
        moveTimerCurrent = moveTimer;

        //Set the idle timer
        idleTimer = UnityEngine.Random.Range(idleTimerMin, idleTimerMax + 1);
        idleTimerCurrent = idleTimer;

        //Set the attack timer
        attackTimerCurrent = attackTimer;

        SetDirection(-1, 1);
    }

    public void CheckAggroRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < aggroRange)
        {
            chasing = true;
        }
        else
        {
            chasing = false;
        }
    }
    public void CheckAttackRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            attacking = true;
            if(!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Anim_Skeleton_Attack"))
            {
                float direction = player.transform.position.x - transform.position.x;
                if (direction > 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
                transform.localScale = new Vector3(direction, transform.localScale.y, 0);
            }
            
        }
        else
        {
            attacking = false;
            GetComponentInChildren<Animator>().SetBool("attacking", false);
        }
    }

    private void SetIdleDirection()
    {
        randMoveX = 0;
        randMoveY = 0;
    }

    private void SetDirection(int min, int max)
    {
        int intervalForRandom = 1;

        randMoveX = Mathf.Floor(UnityEngine.Random.Range(min, max + 1) / intervalForRandom);
        randMoveY = Mathf.Floor(UnityEngine.Random.Range(min, max + 1) / intervalForRandom);

        if (randMoveX == 0 && randMoveY == 0)
        {
            SetDirection(-1, 1);
        }
    }

    private void HandleMovement()
    {
        //Set to idle for duration of idleTimerCurrent
        if (moveTimerCurrent < 0 && idleTimerCurrent > 0)
        {
            //lastXDir = randX;
            SetIdleDirection();
            idleTimerCurrent -= Time.deltaTime;
        }

        //Set to Move once idleTimerCurrent less than zero
        if (moveTimerCurrent < 0 && idleTimerCurrent < 0)
        {
            SetDirection(-1, 1);
            idleTimerCurrent = idleTimer;
            moveTimerCurrent = moveTimer;
        }

        //Move the NPC
        transform.position += new Vector3(randMoveX, randMoveY, 0) * moveSpeed * Time.deltaTime;
        moveTimerCurrent -= Time.deltaTime;
        attackTimerCurrent -= Time.deltaTime;
    }


    private void HandleOrientation()
    {
        if (randMoveX != 0)
        {
            if (randMoveX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void HandleAttack()
    {
        GetComponentInChildren<Animator>().SetBool("attacking", true);

        if (!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Anim_Skeleton_Attack") && attacking == true)
        {
            GetComponentInChildren<Animator>().Play("Anim_Skeleton_Attack");
           
        }
        else
        {
            attacking = false;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();
        if(attacking)
        {
            HandleAttack();
        }
        else
        {
            HandleMovement();
            HandleOrientation();
            anim_controller.HandleAnimation();
        }
        





        /*CheckAggroRange();
        if(chasing)
        {
            Debug.Log("Enemy IN Range");
            chaseDirection = player.transform.position - transform.position;
            chaseDirection.Normalize();
            GetComponent<Rigidbody2D>().velocity = chaseDirection;
        }
        else
        {
            Debug.Log("Enemy OUT of Range");
            HandleMovement();
            HandleOrientation();
            HandleAttack();
            anim_controller.HandleAnimation();
        }*/

    }
}
