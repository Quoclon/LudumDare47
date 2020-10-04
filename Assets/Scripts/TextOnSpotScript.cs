using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnSpotScript : MonoBehaviour {
    [HideInInspector]
    public string DisplayText;      //Gets set by External Script - Currently in bullet.cs
    [HideInInspector]
    public int DisplayPoints;       //Gets set by External Script - Currently in bullet.cs

    public int TextFontSize;
    public Text TextPrefab;
    public float Speed;
    public float DestroyAfter;
    private float Timer;
    private GameObject[] player;
    private GameObject score;
    private bool firstLoop = true;
    public int scoreAmt;



	// Use this for initialization
	void Start () {
        Timer = DestroyAfter;
       // Debug.Log("TextOnSpotScript ********************");
        TextPrefab = GetComponentInChildren<Text>();
        player = GameObject.FindGameObjectsWithTag("PlayerScore");
        //score = player[0].GetComponent<playerScore>().gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;

        if(firstLoop == true)
        {
            //score.GetComponent<playerScore>().increaseScore(DisplayPoints);
            firstLoop = false;
        }

        if (Timer < 0)
        {
            Destroy(gameObject);
        }

        if(DisplayPoints > 0)
        {
            TextPrefab.text = "+" + DisplayPoints;
        }
        else if (DisplayText != null)
        {
            TextPrefab.text = DisplayText;
        }

        TextPrefab.fontSize = TextFontSize;
        if(Speed > 0)
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
            //Debug.Log("TEXTONSPOT " + transform);
        }
    }

    /*
    public void setScoreAmt(int amt)
    {
        scoreAmt = amt;
    }
    */
}
