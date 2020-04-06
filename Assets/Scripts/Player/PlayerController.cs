using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //States
    enum STATES { IDLE, MOVE, TURNING};

    //Components
    public Rigidbody2D      m_rb;
    public Collider2D       m_coll;
    public Animator         m_anim;

    //Player param
    public float            speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // Control player's movement
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");

        if (faceDirection != 0)
        {
            if (faceDirection * transform.localScale.x < 0)
            {
                m_anim.SetBool("Turning", true);
            }
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        if (horizontalMove != 0)
        {
            m_rb.velocity = new Vector2(horizontalMove * speed, m_rb.velocity.y);
            m_anim.SetBool("Moving", true);
            m_anim.SetBool("Idling", false);
        }
        else// if (m_rb.velocity.x == 0)
        {
            m_anim.SetBool("Moving", false);
            m_anim.SetBool("Idling", true);
        }
    }

    void TurningFinished()
    {
        m_anim.SetBool("Turning", false);
    }

}
