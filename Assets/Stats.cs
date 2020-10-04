using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int healthMax;
    public int health;

    public int dmgMax;
    public int dmg;

    public int moveSpeedMax;
    public int moveSpeed;

    public bool deathState;

    private Color entityColor;
    
    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        dmg = dmgMax;
        moveSpeed = moveSpeedMax;

        entityColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamageHealth(1);
        }

    }

    private void CheckDeath()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DamageHealth(int damage)
    {
        health -= damage;
        CheckDeath();
        StartCoroutine(flashColor());
    }

    IEnumerator flashColor()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = entityColor;
    }
}
