using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageFireballTraps : MonoBehaviour
{

   public GameObject[] FireBallSpawners;
    GameManager gameManager;
    [SerializeField]
    float timerCurrent;

    [SerializeField]
    float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timerCurrent = timerMax;
        Debug.Log("Script Ran");
    }

    // Update is called once per frame
    void Update()
    {
        //anim.GetCurrentAnimatorStateInfo(0).IsName("anim_hero_roll_attack")
     
        if (timerCurrent <= 0)
        {
            foreach (var spawner in FireBallSpawners)
            {
                /*
                if (spawner.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Anim_Fireball_Trap") == false){
                    spawner.SetActive(false);
                }
                */

                float random = Random.Range(0, 60);
                if (random <= gameManager.sceneNum)
                {
                    spawner.SetActive(true);
                }
            }
            timerCurrent = timerMax;
        }
        timerCurrent -= Time.deltaTime;
    }
}
