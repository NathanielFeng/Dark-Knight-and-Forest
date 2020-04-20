using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int m_damage;
    private Animator m_anim;
    public GameObject hitObject;
    private PolygonCollider2D m_coll;
    private float m_originX;
    private float m_originY;
    private float m_originZ;
    public AudioClip hitSound;
    public AudioSource source;
    public GameObject player;
    private bool isAttackDown;

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_coll = GetComponent<PolygonCollider2D>();
        m_originX = transform.localPosition.x;
        m_originY = transform.localPosition.y;
        m_originZ = transform.localPosition.z;
        m_coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackFinished()
    {
        m_coll.enabled = false;
        isAttackDown = false;
    }

    public void AttackFront()
    {
        transform.localPosition = new Vector3(m_originX, m_originY, m_originZ);
        m_coll.enabled = true;
        m_anim.SetTrigger("ATK");
    }

    public void AttackUp()
    {
        transform.localPosition = new Vector3(0, m_originY + 1.5f, m_originZ);
        m_coll.enabled = true;
        m_anim.SetTrigger("ATKUP");
    }

    public void AttackDown()
    {
        isAttackDown = true;
        transform.localPosition = new Vector3(0, m_originY - 1.5f, m_originZ);
        m_coll.enabled = true;
        m_anim.SetTrigger("ATKDOWN");
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isAttackDown)
            {
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hurt(m_damage);
            GameObject.Instantiate(hitObject, enemy.HitPosition(), Quaternion.Euler(0f, 0f, 0));
            AudioClip enemyHitSound = enemy.getHitSound();
            source.PlayOneShot(hitSound, 1.0f);
            source.PlayOneShot(enemyHitSound, 1.0f);
        }
    }

}
