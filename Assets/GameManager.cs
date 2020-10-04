using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int sceneNumToLoad;
    public string sceneToLoad;
    public int sceneNum;
    public int NumberOfEnemiesLeft;
  
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
        LoadLoop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNextLoop();
        }
    }

    public void CheckEnemiesRemaining()
    {
        NumberOfEnemiesLeft--;
        if(NumberOfEnemiesLeft <= 0)
        {
            StartNextLoop();
        }
    }

    public void StartNextLoop()
    {
        sceneNum++;
        StartCoroutine(WaitToLoadScene());
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        SceneManager.LoadScene(sceneNumToLoad);
        yield return new WaitForSeconds(0.5f);
        LoadLoop();
    }

    public void RestartLoops()
    {
        sceneNum = 1;
    }

    public void LoadLoop()
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
                    if(enemy.tag == "Enemy")
                    {
                        enemy.gameObject.SetActive(true);
                        NumberOfEnemiesLeft++;
                    }

                    if(enemy.tag == "Trap")
                    {
                        enemy.gameObject.SetActive(true);
                    }

                }
            }
            counter++;
        }
    }
}
