using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Ulti;
    public Transform Firepos;

    GameObject happy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            happy = Instantiate(Bullet, Firepos.transform.position, Firepos.transform.rotation);
            happy.transform.SetParent(gameObject.transform);
        }
        
    }
}
