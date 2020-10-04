﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other != null)
        {
            other.GetComponent<Stats>().DamageHealth(1);
            Debug.Log("ENTER" + other.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other != null)
        {
            //other.GetComponent<Stats>().DamageHealth(1);
            Debug.Log("EXIT" + other.tag);
        }
     
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
