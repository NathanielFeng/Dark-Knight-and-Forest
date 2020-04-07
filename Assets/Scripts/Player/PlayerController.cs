using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
        //Initialized
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        
        if (faceDirection != 0)
        {
            float formerDirction = transform.localScale.x;
            transform.localScale = new Vector3(faceDirection, 1, 1);
            if (formerDirction == transform.localScale.x)
            {
                m_anim.SetBool("Turning", false);
            }
            else
            {
                m_anim.SetBool("Turning", true);
            }
        }
        m_rb.velocity = new Vector2(horizontalMove * speed, m_rb.velocity.y);
        m_anim.SetFloat("HorizontalSpeed", Mathf.Abs(faceDirection));
    }
}
