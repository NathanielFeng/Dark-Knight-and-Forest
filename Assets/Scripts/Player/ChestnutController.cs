using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutController : Enemy
{
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D circleCollider;
    public LayerMask ground;

    //public float jumpForce = 8.0f;
    
    public float speed = 3.0f;
    public float matureAttackDistance;
    public float rollingTime;
    public GameObject leftTopPoint;
    public GameObject rightButtonPoint;

    private Vector2 leftTop;
    private Vector2 rightButton;
    private float faceDirection;
    private float rollingTimeCnt;
    private float nextRollingTimeCnt;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        faceDirection = 1.0f;
        rollingTimeCnt = 0;
        nextRollingTimeCnt = 0;
        getPlayer = false;
        leftTop = new Vector2(leftTopPoint.transform.position.x, rightButtonPoint.transform.position.y);
        rightButton = new Vector2(rightButtonPoint.transform.position.x, rightButtonPoint.transform.position.y);
        Destroy(rightButtonPoint);
        Destroy(leftTopPoint);
    }

    void Update()
    {
        Movement();
        Mature();
    }


    void Movement()
    {
        //角色获得仇恨
        if (getPlayer)
        {
            if (playerObject.transform.position.x > transform.position.x) 
                faceDirection = 1.0f;
            else
                faceDirection = -1.0f;
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        else
        {
            //到达边界转向
            if (transform.position.x < leftTop.x + 1.0f)
                faceDirection = 1.0f;
            else if (transform.position.x > rightButton.x - 1.0f)
                faceDirection = -1.0f;
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        //间隔一段时间后开始移动
        if (nextRollingTimeCnt <= 0f && rollingTimeCnt <= 0)
        {
            rollingTimeCnt = rollingTime;
            nextRollingTimeCnt = 2 * rollingTime;//两次滚动之间的间隔设定
        }
        //在一段时间内移动
        if (rollingTimeCnt > 0)
        {
            rollingTimeCnt -= Time.deltaTime;
            rigid.velocity = new Vector2(faceDirection * speed, rigid.velocity.y);
            //变身后跑动
            if (anim.GetBool("isMature"))
                anim.SetBool("isRolling", true);
            //变身前跑动
            else
                anim.SetBool("isBouncing", true);
        }
        else if (rollingTimeCnt <= 0 && nextRollingTimeCnt > 0) 
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            nextRollingTimeCnt -= Time.deltaTime;
            if (anim.GetBool("isMature"))
                anim.SetBool("isRolling", false);
            else
                anim.SetBool("isBouncing", false);
        }
    }

    void Mature()
    {
        //角色获得仇恨
        if (getPlayer)
        {
            if (!anim.GetBool("isMature") && !anim.GetBool("isBouncing") && MatureAttackCheck())
            {
                anim.SetBool("isMature", true);
                speed *= 2;
                //改变Circle Collider碰撞体的大小
                circleCollider.offset = new Vector2(0.01f, 1.0f);
                circleCollider.radius = 1.0f;
                //改变重力大小
                rigid.gravityScale = 3.0f;
            }
        }
    }

    //进入成熟距离
    bool MatureAttackCheck()
    {
        if(Mathf.Abs(playerObject.transform.position.x - transform.position.x) < matureAttackDistance)
        {
            return true;
        }
        return false;
    }
}
