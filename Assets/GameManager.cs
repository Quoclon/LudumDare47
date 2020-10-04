using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        LoadLoop();
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNextLoop();
            //LoadLoop();
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
        //Debug.Log("StartNextLoop() - SceneNum: " + sceneNum);
        StartCoroutine(loadScene());
        
    }

    IEnumerator loadScene()
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(1f);
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
            //Debug.Log("Length of Loops: " + arrayOfLoops.Length + " Current Loop: " + loop.name + " Num of Enemies in Loop: " + loopEnemies.Length);

            //If 'loop' Level is less than the current loop 
            //(i.e. if loop 3 - Enemies from Loops 1,2,3 will load)
            if (counter < sceneNum)
            {
                foreach (var enemy in loopEnemies)
                {
                    enemy.gameObject.SetActive(true);
                    NumberOfEnemiesLeft++;
                    Debug.Log("Num Enemies Left - In LoadScene(): " + NumberOfEnemiesLeft);
                }
            }
            counter++;
        }
    }
}
