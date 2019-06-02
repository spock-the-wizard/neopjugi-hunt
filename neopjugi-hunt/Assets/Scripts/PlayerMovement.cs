using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;
    public float jumpPower = 0.3f;
    public float height = 0.0f;
    public float speed = 0.05f;
    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;
    public float x = 0.0f;
    public float y = 0.0f;
    public float dist = -4.0f;

    private bool attack;
    private bool jump;
    private bool sprint;
    private bool idle;
    private bool endgame;

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
        endgame = false;

    }

    void Update()
    {
        jump = false;
        sprint = false;
        idle = false;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            attack = true;
        else
        {
            attack = false;
            if (Input.GetButton("Jump"))
                jump = true;
            else
            {
                jump = false;
                if (Input.GetKeyDown("w"))
                    sprint = true;
                else
                {
                    sprint = false;
                    idle = true;
                }
            }
        }

        m_Animator.SetBool("attack", attack);
        m_Animator.SetBool("idle", idle);
        m_Animator.SetBool("fly", jump);
        m_Animator.SetBool("run", sprint);
        m_Animator.SetBool("endgame", endgame);

        View();
        Fly();
        Move();
        Cam();
    }

    void View()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            dist = -4.0f - dist;
        }
    }
    void Move()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(0, x, 0);


        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement = rotation * m_Movement;
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //m_Animator.SetBool("IsWalking", isWalking);

        m_Rotation = Quaternion.Euler(0, x, 0);

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed);
        m_Rigidbody.MoveRotation(m_Rotation);

        float field_boundary = 40.0f;
        if (m_Rigidbody.position.x > field_boundary)
        {
            m_Rigidbody.position = new Vector3(40.0f, m_Rigidbody.position.y, m_Rigidbody.position.z);
        }
        if (m_Rigidbody.position.x < -field_boundary)
        {
            m_Rigidbody.position = new Vector3(-40.0f, m_Rigidbody.position.y, m_Rigidbody.position.z);
        }
        if (m_Rigidbody.position.z > field_boundary)
        {
            m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, m_Rigidbody.position.y, 40.0f);
        }
        if (m_Rigidbody.position.z < -field_boundary)
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
        Camera.main.transform.position = m_Rigidbody.position + rot * new Vector3(0.0f, 0.9f, dist);
        Camera.main.transform.rotation = rot;
    }
    void OnAnimatorMove()
    {
        //m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * m_Animator.deltaPosition.magnitude);
        //m_Rigidbody.MoveRotation(m_Rotation);
    }
    void Fly()
    {
       
        if (jump)
        {
            if (m_Rigidbody.GetPointVelocity(new Vector3(0, 0, 0)).y < 3.0f)
                m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jump = false;
        }
        else
        {
            if (m_Rigidbody.GetPointVelocity(new Vector3(0, 0, 0)).y < -3.0f)
                m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

}
