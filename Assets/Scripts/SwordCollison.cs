using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GetComponentInParent<Stats>().hitEnemy == false)
        {
            other.GetComponent<Stats>().DamageHealth(GetComponentInParent<Stats>().dmgMax);
            GetComponentInParent<Stats>().hitEnemy = true;
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
