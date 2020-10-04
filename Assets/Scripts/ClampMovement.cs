using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampMovement : MonoBehaviour
{
    public bool clamped = false;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public float bonusHeightTop;
    public float bonusHeightBottom;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight * bonusHeightBottom, screenBounds.y - objectHeight * bonusHeightTop);
        transform.position = viewPos;
    }

}