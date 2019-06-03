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
    public float dist = -4.0f;
    public Vector3 viewtarget;
    private bool jump;
    public Camera main;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        View();
        Fly();
        Move();
        Cam();
    }

    void View()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            dist = -2.0f - dist;
        }
    }
    void Move()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(0, x, 0);

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //float horizontal =0;

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

        //bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        //bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        //bool isWalking = hasHorizontalInput || hasVerticalInput;
        //bool isWalking = Input.GetKey("w");

        // m_Animator.SetBool("run", isWalking);

        bool idle;
        bool isWalking;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            m_Animator.Play("attack");
        }
        else
        {
            if (Input.GetKey("space"))//fly
            {

                m_Animator.Play("fly");
            }
            else
            {
                //if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
                if (Input.GetKey("w"))
                {
                    m_Animator.Play("run");

                }
                else
                {
                    m_Animator.Play("idle");

                }
            }
        }


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
        if (m_Rigidbody.position.y > 10.0f)
        {
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, 10.0f, m_Rigidbody.position.z);
        }
    }
    void Cam()
    {
        Quaternion rot = Quaternion.Euler(y, x, 0);
        viewtarget = m_Rigidbody.position + rot * new Vector3(0.0f, 3.0f, 2.0f);

        Camera.main.transform.position = m_Rigidbody.position + rot * new Vector3(0.0f, 3.0f, dist);
        Camera.main.transform.rotation = rot;
    }
    void OnAnimatorMove()
    {
        //m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * m_Animator.deltaPosition.magnitude);
        //m_Rigidbody.MoveRotation(m_Rotation);
    }
    void Fly()
    {
        if (Input.GetButton("Jump"))
        {
            jump = true;
           Debug.Log("jumping)");
        }
        //else
           // jump = false;
        // m_Animator.SetBool("fly", jump);
        float falling_coefficient = 3.0f;
        
        
        if (jump)
        {
            if (m_Rigidbody.GetPointVelocity(new Vector3(0, 0, 0)).y < 3.0f)
                m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jump = false;
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