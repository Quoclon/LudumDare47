using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;

    public int sceneNumToLoad;
    public string sceneToLoad;
    public int sceneNum;
    public int NumberOfEnemiesLeft;

    public bool firstLoop = true;
    
    [Header("**** UI - Text ****")]
    public GameObject StartLoopText;
    public GameObject BeatLoopText;
    public GameObject BeatGameText;
    public GameObject GameOverText;


    //Game Manager Singleton + Awake()
    static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(sceneToLoad == "Blake")
        {
            sceneNumToLoad = 1;
        }
        else
        {
            sceneNumToLoad = 0;
        }
       
        StartNextLoop();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNextLoop();
        }
    }

    //Load Next Scene Functions
    public void StartNextLoop()
    {
        if (!firstLoop) {
            sceneNum++;
        }
        DisableEnemies();
        DisableTraps();
        DisableFireballs();
        StartCoroutine(WaitToLoadScene());
    }

    IEnumerator WaitToLoadScene()
    {
        if (!firstLoop)
        {
            BeatLoopText.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            yield return new WaitForSeconds(0f);

        }
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        SceneManager.LoadScene(sceneNumToLoad);
        if (!firstLoop)
        {
            BeatLoopText.SetActive(false);
            StartLoopText.GetComponent<TextMeshProUGUI>().text = "Loop " + sceneNum;
            StartLoopText.SetActive(true);
            StartCoroutine(RemoveLoadSceneText());
            yield return new WaitForSeconds(.5f);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
        }

        LoadLoop();

    }

    IEnumerator RemoveLoadSceneText()
    {
        yield return new WaitForSeconds(2f);
        StartLoopText.SetActive(false);
    }

    //Function that runs to configure each loop
    void LoadLoop()
    {
        //DisableTraps();
        //DisableFireballs();
        //DisableEnemies();
        LoadEnemies();
        if (firstLoop)
        {
            firstLoop = false;
        }
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.playAudio("StartMusic");
    }

    IEnumerator RestartLoops()
    {
        sceneNum = 1;
        firstLoop = true;
        yield return new WaitForSeconds(4f);
        GameOverText.SetActive(false);
        DisablePlayer();
        StartNextLoop();
    }

    void LoadEnemies()
    {
        //Check for ALL objects with the name of Loop 
        GameObject[] arrayOfLoops = GameObject.FindGameObjectsWithTag("Loop");

        //Loop through ALL loop 'levels
        NumberOfEnemiesLeft = 0;
        var counter = 0;
        foreach (var loop in arrayOfLoops)
        {
            //Check for Enemies within the Loop Object Container(i.e. Loop2)
            Rigidbody2D[] loopEnemies = loop.GetComponentsInChildren<Rigidbody2D>(true);

            //If 'loop' Level is less than the current loop 
            //(i.e. if loop 3 - Enemies from Loops 1,2,3 will load)
            if (counter < sceneNum)
            {
                foreach (var enemy in loopEnemies)
                {
                    if (enemy.tag == "Enemy")
                    {
                        enemy.gameObject.SetActive(true);
                        NumberOfEnemiesLeft++;
                    }

                    if (enemy.tag == "Trap")
                    {
                        //enemy.gameObject.SetActive(true);
                    }

                }
            }
            counter++;
        }
    }

    void DisableTraps()
    {
        GameObject[] arrayOfTraps = GameObject.FindGameObjectsWithTag("FireballTrap");
        foreach (var trap in arrayOfTraps)
        {
            trap.SetActive(false);
        }

        GameObject[] arrayOfFireBallManagers = GameObject.FindGameObjectsWithTag("FireballManager");
        foreach (var manager in arrayOfFireBallManagers)
        {
            manager.SetActive(false);
        }
    }

    void DisableFireballs()
    {
        GameObject[] arrayOfFireballs = GameObject.FindGameObjectsWithTag("Fireball");
        foreach (var fireball in arrayOfFireballs)
        {
            fireball.SetActive(false);
        }
    }

    void DisableEnemies()
    {
        GameObject[] arrayOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(arrayOfEnemies.Length > 0)
        {
            foreach (var enemy in arrayOfEnemies)
            {
                enemy.SetActive(false);
            }
        }
   
    }

    void DisablePlayer()
    {
        GameObject[] arrayOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (arrayOfPlayers.Length > 0)
        {
            foreach (var player in arrayOfPlayers)
            {
                player.SetActive(false);
            }
        }

    }

    public void CheckEnemiesRemaining()
    {
        NumberOfEnemiesLeft--;
        if (NumberOfEnemiesLeft <= 0)
        {
            StartNextLoop();
        }
    }

    /// <summary>
    /// Handle Text Stuff
    /// </summary>
    public void GameOver()
    {
        Debug.Log("Game Over Ran");
        DisableTraps();
        DisableFireballs();
        DisableEnemies();
        GameOverText.SetActive(true);
        audioManager.stopAudio("StartMusic");
        StartCoroutine(RestartLoops());
    }

    public void BeatGame()
    {
        //BeatGameText.enabled = false;
    }



}
