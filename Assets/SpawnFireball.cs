using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireball : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject gm = Instantiate(Resources.Load("Fireballz") as GameObject);
        gm.transform.position = this.transform.position;
        gm.transform.localScale = new Vector3(this.transform.localScale.x/2, 0.5f, 0);


        //Debug.Log(this.GetComponentInParent<Transform>().GetComponentInParent<Transform>().localScale.x);

        //if (this.GetComponentInParent<Transform>().GetComponentInParent<Transform>().localScale.x < 0)
        //{
           // gm.transform.localScale *= gm.transform.localScale.x * -1;
        //}
    }
}
