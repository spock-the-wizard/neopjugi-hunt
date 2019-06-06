using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;
    public float jumpPower = 0.3f;
    public float height = 0.0f;
    public float speed = 2.0f;
    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;
    public float horizontal = 0;
    public float vertical = 0;
    public float x = 0.0f;
    public float y = 0.0f;
    public float disty = 4.0f;
    public float distz = -6.0f;
    public Vector3 viewtarget;
    private bool jump;
    public GameObject neop;
    public Camera main;
    public GameObject fireball;
    public Time t;

    private bool startgame = false;
    public CanvasGroup trans;

    //Camera main;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    
    int neop_num = 5;
    Vector3 minpos = new Vector3(-10,0,-10);
    Vector3 maxpos = new Vector3(10, 0, 100);
    

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);

    }

    void Start()
    {
        /*for(int i=0;i<neop_num;i++)
        {
            Vector3 randpos = new Vector3(Random.Range(minpos.x, maxpos.x), Random.Range(minpos.y, maxpos.y), Random.Range(minpos.z, maxpos.z));
            GameObject monster = Instantiate(neop);
            monster.transform.position = randpos;
        }*/

        
    }

    void Update()
    {
        StartGame();
        if (startgame == true)
        {
            View();
            Fly();
            Cam();
            Move();
            Attack();
        }
        
    }

    void StartGame()
    {
        if(Input.GetKey("space"))
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            startgame = true;

            trans = GetComponent<CanvasGroup>();
            trans.alpha = 0;
        }
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(1))
        {
            float speed=1;
            Quaternion rot = Quaternion.Euler(y, x, 0);
            //float y_max = 3;
            //float y_min = -5;
            Vector3 temp = m_Rigidbody.position;
            Vector3 pos = viewtarget + rot * new Vector3(0.0f, 0.0f, 13.0f);
            //viewtarget + new Vector3(0, -2, 0) ;//gameObject.transform.position + new Vector3(0, 2, 0);
            Vector3 vel = speed * (pos - gameObject.transform.position);
                //speed * (viewtarget - gameObject.transform.position)+(new Vector3(0,-1,0));
            

            GameObject fire =Instantiate(fireball,viewtarget,gameObject.transform.rotation);
            
            //vel.y = 0;
            fire.GetComponent<Rigidbody>().velocity = vel;
            fire.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(vel);
            //fire.transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0));
            Destroy(fire, 5);
        }
    }
  
    void View()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            disty = 7.0f - disty;
            distz = -4.0f - distz;
        }
    }

    void Move()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(0, x, 0);

        float increment = 3;
        if (Input.GetKey("w"))
        {
            vertical = increment;
        }
        else if (Input.GetKey("s"))
            vertical = -increment;
        else
            vertical = 0;

        m_Movement.Set(horizontal , 0f, vertical);
        m_Movement = rotation * m_Movement;
        //m_Movement.Normalize();

        // m_Animator.SetBool("run", isWalking);
        
        if ((Input.GetKey("w") || Input.GetKey("s")) && m_Rigidbody.position.y <= 1.0f)
        {
            m_Animator.Play("run");
        }
        else
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                m_Animator.Play("attack");
            }
            else
            {
                if (jump)//fly
                {
                    m_Animator.Play("fly");
                }
                else if(m_Rigidbody.position.y <= 0.1f)
                {
                    m_Animator.Play("idle");
                }
            }
        }

        /*
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            m_Animator.Play("attack");
        }
        else
        {
            if (jump)//fly
            {

                m_Animator.Play("fly");
            }
            else
            {
                //if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
                if (Input.GetKey("w") || Input.GetKey("s"))
                {
                    m_Animator.Play("run");

                }
                else
                {
                    m_Animator.Play("idle");

                }
            }
        }
        */

        m_Rotation = Quaternion.Euler(0, x, 0);

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed);
        m_Rigidbody.MoveRotation(m_Rotation);

        if (m_Rigidbody.position.x > 40.0f)
        {
            m_Rigidbody.position = new Vector3(40.0f, m_Rigidbody.position.y, m_Rigidbody.position.z);
        }
        if (m_Rigidbody.position.x < -40.0f)
        {
            m_Rigidbody.position = new Vector3(-40.0f, m_Rigidbody.position.y, m_Rigidbody.position.z);
        }
        if (m_Rigidbody.position.z > 40.0f)
        {
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, m_Rigidbody.position.y, 40.0f);
        }
        if (m_Rigidbody.position.z < -40.0f)
        {
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, m_Rigidbody.position.y, -40.0f);
        }
        if (m_Rigidbody.position.y > 20.0f)
        {
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, 20.0f, m_Rigidbody.position.z);
        }
    }
    void Cam()
    {
        Quaternion rot = Quaternion.Euler(y, x, 0);
        viewtarget = m_Rigidbody.position + rot * new Vector3(0.0f, 3.0f, 2.0f);

        Camera.main.transform.position = m_Rigidbody.position + rot * new Vector3(0.0f, disty, distz);
        Camera.main.transform.rotation = rot;
    }
    void OnAnimatorMove()
    {
        //m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * m_Animator.deltaPosition.magnitude);
        //m_Rigidbody.MoveRotation(m_Rotation);
    }
    void Fly()
    {
        if (Input.GetKey("space"))
        {
            jump = true;   
        }
        else
            jump = false;
       
        float falling_coefficient = 3.0f;
        
        
        if (jump)
        {
            if (m_Rigidbody.GetPointVelocity(new Vector3(0, 0, 0)).y < 3.0f)
                m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            //jump = false;
        }
        else
        {
            if (m_Rigidbody.GetPointVelocity(new Vector3(0, 0, 0)).y < -3.0f)
            {
                m_Rigidbody.AddForce(Vector3.up * jumpPower * falling_coefficient, ForceMode.Impulse);
            }
        }
    }

}