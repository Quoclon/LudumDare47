using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFireball : MonoBehaviour
{
    
    [SerializeField] float moveSpeed;
    [SerializeField] int dmg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(transform.localScale.x, 0, 0) * moveSpeed * Time.deltaTime; 
    }
}
