using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public int Damage = 1;
    public float Speed = 1000.0f;
    public Camera main;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * Speed);
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
