using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public Rigidbody ball;

    // Start is called before the first frame update
    
    void Start()
    {
        float coefficient = 20;
        ball.velocity = coefficient*(gameObject.GetComponentInParent<PlayerMovement>().viewtarget-ball.position);
        //ball.velocity = new Vector3(0, 0, 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collion!");
        if(col.gameObject.tag!="Player")
            Destroy(gameObject,1.0f);
    }
}
