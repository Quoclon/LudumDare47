using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    private Vector2 screenBounds;


    public GameObject[] EnemyTypes;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void Spawn()
    { 
        GameObject enemy = Instantiate(EnemyTypes[0]);
        float enemyWidth;
        float enemyHeight;
        enemyWidth = enemy.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        enemyHeight = enemy.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
        Vector3 spawnPos = new Vector3(
            UnityEngine.Random.Range((-screenBounds.x + enemyWidth), (screenBounds.x - enemyWidth)),
            UnityEngine.Random.Range((-screenBounds.y + enemyHeight), screenBounds.y - enemyHeight),
            0);
        enemy.transform.position = spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Spawn();
        }
    }
}
