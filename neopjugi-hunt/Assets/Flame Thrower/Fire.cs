﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log("collide");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
