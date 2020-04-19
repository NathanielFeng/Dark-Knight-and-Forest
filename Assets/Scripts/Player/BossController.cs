using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D circleCollider;
    public GameObject damageTrigger;
    public GameObject player;
    public GameObject flame;
    public Transform shotPoint;
    public Slider bloodBar;

    //public LayerMask ground;
    //public float jumpForce = 8.0f;

    [Header("Attributes")]
    public float speed;
    private float faceDirection;
    public float health = 100.0f;
    private float attackTime = 0.2f;
    private float attackTimeCnt;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        Mature();
        attackTimeCnt = attackTime;
    }

    void Update()
    {
        bloodBar.value = health;
        if (health > 50 || health < 25)
            Attack1();
        else
            Attack2();
    }


    void Attack1()
    {
        rigid.gravityScale = 3.0f;
        if (player.transform.position.x - transform.position.x > 5.0f)
            faceDirection = 1.0f;
        if (transform.position.x - player.transform.position.x > 5.0f) 
            faceDirection = -1.0f;
        transform.localScale = new Vector3(faceDirection, 1, 1);
        rigid.velocity = new Vector2(faceDirection * speed * Time.deltaTime, rigid.velocity.y);
        anim.SetBool("isRolling", true);
    }

    void Attack2()
    {
        rigid.gravityScale = 0.0f;
        transform.position = new Vector2(transform.position.x, player.transform.position.y + 10.0f);
        if (player.transform.position.x - transform.position.x > 5.0f)
            faceDirection = 1.0f;
        if (transform.position.x - player.transform.position.x > 5.0f)
            faceDirection = -1.0f;
        transform.localScale = new Vector3(faceDirection, 1, 1);

        rigid.velocity = new Vector2(faceDirection * speed * Time.deltaTime, 0);

        //每隔一段时间攻击
        if (attackTimeCnt <= 0)
        {
            Instantiate(flame, shotPoint.position, Quaternion.Euler(0f, 0f, -170f));
            attackTimeCnt = attackTime;
        }
        else
            attackTimeCnt -= Time.deltaTime;

    }


    void Mature()
    {
        if (!anim.GetBool("isMature"))
        {
            anim.SetBool("isMature", true);
            speed *= 2;
            //改变Circle Collider碰撞体的大小
            circleCollider.offset = new Vector2(0.01f, 1.0f);
            circleCollider.radius = 1.0f;
            //改变重力大小
            rigid.gravityScale = 3.0f;

            //改变damageTrigger碰撞体的大小
            damageTrigger.GetComponent<CircleCollider2D>().offset = new Vector2(0.01f, 1.0f);
            damageTrigger.GetComponent<CircleCollider2D>().radius = 1.0f;
        }
    }
}
