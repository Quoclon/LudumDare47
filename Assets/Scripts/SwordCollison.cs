using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");
        //Vector3 otherCurrentPosition = other.transform.TransformVector;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXIT");
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
