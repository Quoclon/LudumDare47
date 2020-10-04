using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveInput;
    public Rigidbody2D theRB;
    public Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Store moveInput in the Horizontal direction. Horizontal references the Unity Input 'Horizonal' defaults
        if(stats.health > 0)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");

            // Store moveInput in the Vertical direction. Vertical references the Unity Input 'Vertical' defaults
            moveInput.y = Input.GetAxisRaw("Vertical");

            // Smooth out player speed in diagonal directions
            moveInput.Normalize();

            // Change Player Direction. Local Scale to -1 if Keypress is A, else change Local Scale to 1 if Keypress is D
            if (moveInput.x == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (moveInput.x == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // Multiply the RB position vector by moveSpeed to get current velocity
            theRB.velocity = moveInput * stats.moveSpeed;
        }
        

    }
}
