using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class condition : MonoBehaviour
{
    public ParticleSystem _psystem;

    // Start is called before the first frame update
    void Start()
    {
        _psystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        _psystem = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        _psystem.Play();
    }
}
