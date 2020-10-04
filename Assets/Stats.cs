using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int healthMax;
    public int health;

    public int dmgMax;
    public int dmg;

    public int energyMax;
    public int energy;

    public float moveSpeedMax;
    public float moveSpeed;
    public float moveSpeedRolling;

    public bool deathState;

    public bool hitEnemy = false;

    private Color entityColor;

    AudioManager audioManager;
    public string nameForAudio;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        entityColor = GetComponentInChildren<SpriteRenderer>().color;
        gameManager = FindObjectOfType<GameManager>();

        health = healthMax;
        dmg = dmgMax;
        moveSpeed = moveSpeedMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamageHealth(50);
            Debug.Log(gameObject.tag + " took " + health + " dmg");
        }

    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                Debug.Log("Player Died");
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
        health -= damage;
        if (gameObject.tag == "Player")
        {
            CheckDeath();
        }
        else if(gameObject.name == "Skeleton")
        {
            audioManager.playAudio("SkeletonHit");
            StartCoroutine(flashColor());
            CheckDeath();
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
