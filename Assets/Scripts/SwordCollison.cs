using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null &&  other.tag != "Player")
        {
            other.GetComponent<Stats>().DamageHealth(1);
            Debug.Log("ENTER: " + other.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            Debug.Log("EXIT: " + other.transform.position);
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
