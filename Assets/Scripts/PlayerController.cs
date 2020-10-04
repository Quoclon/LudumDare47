using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Player Movement Variables
    public float moveSpeed;
    public float moveSpeedCurrent;
    public float moveSpeedRolling;
    public Vector2 moveInput;
    public Rigidbody2D theRB;

    // Player Stat Variables
    public int health = 100;

    // Other scripts can use this to set the current moveSpeed variable
    public void setMoveSpeedCurrent(float ms)
    {
        moveSpeedCurrent = ms;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedCurrent = moveSpeed;
        moveSpeedRolling = moveSpeed * 2f;
    }

    // Update is called once per frame
    void Update()
    {

        // Store moveInput in the Horizontal direction. Horizontal references the Unity Input 'Horizonal' defaults
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // Store moveInput in the Vertical direction. Vertical references the Unity Input 'Vertical' defaults
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Smooth out player speed in diagonal directions
        moveInput.Normalize();

        // Change Player Direction. Local Scale to -1 if Keypress is A, else change Local Scale to 1 if Keypress is D
        if(moveInput.x == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(moveInput.x == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Add on to the Player's current Position with a new Transform: Position using values from moveInput x & y
        // Multiply each input by Time.deltaTime to smooth out movement
        //transform.position += new Vector3(moveInput.x, moveInput.y, 0f) * Time.deltaTime * moveSpeedCurrent;
        theRB.velocity = moveInput * moveSpeedCurrent;

    }
}
