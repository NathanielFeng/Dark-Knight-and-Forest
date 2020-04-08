using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Components
    private Rigidbody2D     m_rb;
    private Animator        m_anim;
    public Collider2D       m_coll;
    public LayerMask        m_ground;

    //Player param
    public float            m_speed;
    public float            m_jumpForce;
    public float floatingWindows;

    //Player private info
    private bool m_isJumping;
    private bool m_isFloating;
    private bool m_isFalling;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    //########################################
    //#            PLAYER FUNCTION           #
    //########################################

    //Initialized Function
    void Initialize()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    // Control player's movement
    void Movement()
    {
        //Initialized
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");

        //Turn around
        if (faceDirection != 0)
        {
            float formerDirection = transform.localScale.x;
            //Set new direction
            transform.localScale = new Vector3(faceDirection, 1, 1);
            //If player face direction is changed
            if (formerDirection == transform.localScale.x)
            {
                m_anim.SetBool("Turning", false);
            }
            else
            {
                m_anim.SetBool("Turning", true);
            }
        }
        m_rb.velocity = new Vector2(horizontalMove * m_speed, m_rb.velocity.y);
        m_anim.SetFloat("HorizontalSpeed", Mathf.Abs(faceDirection));
    }

    //Player jump
    void Jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpForce);
            m_anim.SetBool("Jumping", true);
            m_isJumping = true;
        }
        //Float
        else if (m_isJumping && m_rb.velocity.y < floatingWindows && m_rb.velocity.y > -1.0 * floatingWindows)
        {
            m_isJumping = false;
            m_isFloating = true;
            m_anim.SetBool("Jumping", false);
            m_anim.SetBool("Floating", true);
        }
        //Fall
        else if (m_isFloating && m_rb.velocity.y < -1.0 * floatingWindows)
        {
            m_isFloating = false;
            m_isFalling = true;
            m_anim.SetBool("Floating", false);
            m_anim.SetBool("Falling", true);
        }
        //On the ground
        else if (m_isFalling && m_coll.IsTouchingLayers(m_ground))
        {
            m_isFalling = false;
            m_anim.SetBool("Falling", false);
        }
    }
}
