using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ball")
        {
            Debug.Log(collision.collider.tag);
            Destroy(gameObject, 3);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag =="Bullet")
        Destroy(gameObject, 2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
