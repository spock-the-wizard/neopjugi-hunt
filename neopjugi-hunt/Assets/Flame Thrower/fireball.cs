using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public PlayerMovement character;

    public ParticleSystem fire;
    public ParticleSystem trail1;
    public ParticleSystem trail2;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        fire.transform.LookAt(character.viewtarget);
        //trail1.transform.LookAt(gameObject.GetComponentInParent<PlayerMovement>().viewtarget);
        //trail2.transform.LookAt(gameObject.GetComponentInParent<PlayerMovement>().viewtarget);
        if (Input.GetMouseButton(1))
        {
            fire.Emit(1);
            trail1.Emit(10);
            trail2.Emit(6);
        }
    }
}
