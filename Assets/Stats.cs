using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float healthMax;
    public float health;

    public int dmgMax;
    public int dmg;

    public float energyMax;
    public float energy;
    public float energyCost;
    public float energyGainPerSec;

    public bool isRolling = false;

    public float moveSpeedMax;
    public float moveSpeed;
    public float moveSpeedRolling;

    public bool deathState;

    public bool hitEnemy = false;

    private Color entityColor;

    AudioManager audioManager;
    public string nameForAudio;
    GameManager gameManager;

    HealthBarScript healthbar;
    EnergyBarScript energybar;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        entityColor = GetComponentInChildren<SpriteRenderer>().color;
        gameManager = FindObjectOfType<GameManager>();
        healthbar = FindObjectOfType<HealthBarScript>();
        energybar = FindObjectOfType<EnergyBarScript>();

        health = healthMax;
        dmg = dmgMax;
        moveSpeed = moveSpeedMax;
        energy = energyMax;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            //DamageHealth(50);
            //Debug.Log(gameObject.tag + " took " + health + " dmg");
        }

        if (gameObject.tag == "Player")
        {
            healthbar.SetMaxHealth(healthMax);
            healthbar.SetHealth(health);
        }

        if (gameObject.tag == "Player")
        {    
            energybar.SetMaxEnergy(energyMax);
            energybar.SetEnergy(energy);
            energy += energyGainPerSec * Time.deltaTime;
            if(energy > energyMax)
            {
                energy = energyMax;
            }

            //Debug.Log(isRolling);

        }


    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                //Debug.Log("Player Died");
                gameManager.GameOver();
            }
            else
            {
                audioManager.playAudio("Death" + nameForAudio);
                deathState = true;
                //Debug.Log("Enemy Died");
                Destroy(this.gameObject);
                gameManager.CheckEnemiesRemaining();
            }
        }
    }

    public void DamageHealth(int damage)
    {

        if (!isRolling)
        {
            health -= damage;
        }
       

        if (gameObject.tag == "Player")
        {
            if(health < 0)
            {
                health = 0;
            }

            audioManager.playAudio("PlayerHit");
            StartCoroutine(flashColor());
            CheckDeath();
        }
        else if(gameObject.tag == "Enemy")
        {
            audioManager.playAudio("SkeletonHit");
            StartCoroutine(flashColor());
            CheckDeath();
        }
    }

    public void ReduceEnergy()
    {
        energy -= energyCost;
        if(energy < 0)
        {
            energy = 0;
        }
    }

    public void UpdateMoveSpeed(float ms)
    {
        moveSpeed = ms;
    }

    IEnumerator flashColor()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = entityColor;
    }
}
