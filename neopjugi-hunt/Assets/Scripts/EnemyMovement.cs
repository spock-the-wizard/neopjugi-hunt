using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int hp = 50;
    public int move;
    public int dir;
    public float speed = 0.05f;

    public ParticleSystem _psystem;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Animator m_Animator;
    Quaternion m_Rotation = Quaternion.identity;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.tag);
        if (col.collider.tag == "ball")
        {
            hp-=10;
            
            if (_psystem)
            {
                if (_psystem.isPlaying)
                {
                    _psystem.Stop();
                    _psystem.Clear();
                    _psystem.Play();
                }
                else
                {
                    _psystem.Play();
                }
            }
            else
            {
                Vector3 pos = Vector3.zero;
                pos.y = 3.0f;

                Transform particleObject = (Transform)Instantiate(Resources.Load("Assets/ExplosiveRealFree/Example/Explosion.prefab", typeof(Transform)), pos, Quaternion.identity);
                _psystem = (ParticleSystem)particleObject.GetComponent(typeof(ParticleSystem));
            }
            if (hp <= 0) Destroy(gameObject);
            Destroy(col.gameObject, 5);
        }
    }

    private void OnParticleCollision(GameObject other)
    {

        if (other.tag == "Bullet")
        {
            hp--;
            if (hp <= 0) Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        dir = Random.Range(0, 4);
        move = 0;
        m_Animator.SetBool("IsWalking", true);
        _psystem.Stop();
        _psystem.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (move >= 100)
        {
            dir = Random.Range(0, 4);
            move = 0;
        }
        else
        {
            move++;
            if(dir == 0)
            {
                m_Movement = new Vector3(1.0f, 0.0f, 0.0f);
            }
            else if(dir == 1)
            {
                m_Movement = new Vector3(-1.0f, 0.0f, 0.0f);
            }
            else if (dir == 2)
            {
                m_Movement = new Vector3(0.0f, 0.0f, 1.0f);
            }
            else if (dir == 3)
            {
                m_Movement = new Vector3(0.0f, 0.0f, -1.0f);
            }
            
            if((m_Rigidbody.position+m_Movement*speed).x < 40.0f && 
                (m_Rigidbody.position + m_Movement * speed).x > -40.0f && 
                (m_Rigidbody.position + m_Movement * speed).z < 40.0f && 
                (m_Rigidbody.position + m_Movement * speed).z > -40.0f)
                m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed);
        }
    }
}
