﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit Enemy: " + other.tag);
        other.GetComponent<Stats>().DamageHealth(1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            //Debug.Log("EXIT" + other.tag);
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
