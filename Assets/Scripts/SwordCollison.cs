using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Stats>().currentlyHit == false)
        {
            other.GetComponent<Stats>().DamageHealth(GetComponentInParent<Stats>().dmgMax);
            other.GetComponent<Stats>().currentlyHit = true;
            Debug.Log("Hit: " + other.tag);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Stats>().currentlyHit = false;

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
