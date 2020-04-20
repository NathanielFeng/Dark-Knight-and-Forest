using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : Enemy
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
    public float attackTime;
    public int phaseTime;//修改成按次数计算攻击方式
    public float nextMoveTime;
    private float attackTimeCnt;
    private int phaseTimeCnt;
    private float nextMoveTimeCnt;
    private float maxHealth;
    private bool nextPhase;
    private Vector3 difference;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        nextPhase = false;
        maxHealth = health;
        attackTimeCnt = attackTime;
        phaseTimeCnt = phaseTime;
        nextMoveTimeCnt = 0;
        rigid.gravityScale = 3.0f;
    }

    void Update()
    {
        StateCheck();
        HurtIntervalTimmer();
    }

    void StateCheck()
    {
        bloodBar.value = health;
        //受伤状态不攻击
        if (isHurt)
        {
            return;
        }
        if(isDead)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
            Invoke("returnToMainMenu", 2.0f);
            return;
        }

        //二阶段
        if (!nextPhase && health <= 0.3 * maxHealth) 
        {
            phaseTime -= 2;
            speed = 2.0f * speed;
            nextMoveTime *= 0.75f;
            nextPhase = true;
        }
        if (health > 0)
        {
            if(Mathf.Abs(transform.position.x - player.transform.position.x) > 10f)
            {
                Attack2();
            }
            else if (phaseTimeCnt > 0 && attackTimeCnt < 0)
            {
                int RandKey = Random.Range(0, 2);
                if (RandKey == 0)
                    Attack1();
                else
                    Attack3();
            }
            else if (phaseTimeCnt <= 0 && nextMoveTimeCnt < 0 && attackTimeCnt<0)
            {
                Attack2();
            }
        }
        if (nextMoveTimeCnt > 0)
        {
            nextMoveTimeCnt -= Time.deltaTime;
        }
        if (attackTimeCnt > 0) 
        {
            attackTimeCnt -= Time.deltaTime;
        }
    }

    void Attack1()
    {
        //两次移动之间的间隔
        if (nextMoveTimeCnt <= 0)
        {
            //进行一个完整的移动时不允许转身
            if (!anim.GetBool("isMoving") && !(Mathf.Abs(rigid.velocity.x) > 0.1f))
            {
                if (player.transform.position.x - transform.position.x > 1.0f)
                    faceDirection = 1.0f;
                if (transform.position.x - player.transform.position.x > 1.0f)
                    faceDirection = -1.0f;
                transform.localScale = new Vector3(faceDirection, 1, 1);
            }
            rigid.velocity = new Vector2(faceDirection * speed, 13f);
            anim.SetBool("isMoving", true);
            nextMoveTimeCnt = nextMoveTime;
            attackTimeCnt = attackTime;
            phaseTimeCnt -= 1;
        }
    }

    void Attack2()
    {
        rigid.gravityScale = 3.0f;
        if (attackTimeCnt <= 0)
        {            
            //transform.position = new Vector2(transform.position.x, player.transform.position.y + 2.5f);
            //确定方向
            if (player.transform.position.x - transform.position.x > 1.0f)
                faceDirection = 1.0f;
            if (transform.position.x - player.transform.position.x > 1.0f)
                faceDirection = -1.0f;
            transform.localScale = new Vector3(faceDirection, 1, 1);
            //跳起来吐火
            rigid.velocity = new Vector2(faceDirection + 1f, 17f);
            anim.SetBool("isFiring", true);
            attackTimeCnt = attackTime;
            phaseTimeCnt = phaseTime;
        }
    }

    void Attack3()
    {
        if (nextMoveTimeCnt <= 0)
        {
            //进行一个完整的冲刺时不允许转身
            if (!anim.GetBool("isDashing") && !(Mathf.Abs(rigid.velocity.x) > 0.1f))
            {
                if (player.transform.position.x - transform.position.x > 1.0f)
                    faceDirection = 1.0f;
                if (transform.position.x - player.transform.position.x > 1.0f)
                    faceDirection = -1.0f;
                transform.localScale = new Vector3(faceDirection, 1, 1);
            }
            rigid.velocity = new Vector2(faceDirection * speed * 1.5f, 0f);
            anim.SetBool("isDashing", true);
            nextMoveTimeCnt = nextMoveTime;
            attackTimeCnt = attackTime;
            phaseTimeCnt -= 1;
        }
    }

    void GetPlayerPos()
    {
        difference = player.transform.position - transform.position;
    }

    void MovingFinished()
    {
        anim.SetBool("isMoving", false);
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void DashingFinished()
    {
        anim.SetBool("isDashing", false);
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void FiringFinished()
    {
        anim.SetBool("isFiring", false);
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void Fire()
    {
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 92.0f;
        rotZ += Random.Range(-10.0f, 10.0f);
        if (health > maxHealth * 0.5f)
        {
            Instantiate(flame, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ));//-170f
        }
        else
        {
            float axis = -30f;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(flame, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + axis));
                axis += 30f;
            }
        }
    }

    override protected void HurtBehavior()
    {
        anim.SetBool("isFiring", false);
        anim.SetBool("isDashing", false);
        anim.SetBool("isMoving", false);
        anim.SetTrigger("isInjured");
        if(player.transform .position.y - transform.position.y < -1f)
            rigid.velocity = new Vector2(0, /*-1f * rigid.velocity.y +*/ 13f);
        else
            rigid.velocity = new Vector2(player.transform.localScale.x * 15f, 0f);
    }

    override protected void DeadBehavior()
    {
        anim.SetBool("isFiring", false);
        anim.SetBool("isDashing", false);
        anim.SetBool("isMoving", false);
        anim.SetBool("isDead", true);
        Destroy(damageTrigger);
    }

    void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
