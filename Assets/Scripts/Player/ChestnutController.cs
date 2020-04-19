using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestnutController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D circleCollider;
    public GameObject player;
    public Slider bloodBar;
    public LayerMask ground;

    [Header("Attributes")]
    public float input;
    public float jumpForce = 8.0f;
    private float faceDirection;
    public float speed = 100.0f;
    public float health = 100.0f;
    public string transformKey;

    [Header("testForCode")]
    public string hitKey;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
      
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");
        //朝向
        if (faceDirection != 0)
            transform.localScale = new Vector3(faceDirection, 1, 1);

        battle();

        changeAnimation();

        bloodBar.value = health;


    }

    void FixedUpdate()
    {
        //移动
        rigid.velocity = new Vector2(input * speed * Time.fixedDeltaTime, rigid.velocity.y);
        //跳起
        if (Input.GetButtonDown("Jump") && circleCollider.IsTouchingLayers(ground) && anim.GetBool("isRolling"))
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }

    //boss战斗
    void battle()
    {
        //1阶段
        if(health > 75)
        {
            if(Input.GetKeyDown(hitKey))
            {
                //向上移动
                transform.position = new Vector2(transform.position.x, player.transform.position.y);
                rigid.gravityScale = 0f;    
                Invoke("attack1", 1.0f);
                
            }
        }
        //2阶段
        else if(health > 25)
        {

        }
        //3阶段
        else
        {

        }
    }

    void attack1()
    {
        Vector2 oldPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(player.transform.position, oldPosition, speed * Time.deltaTime);
        rigid.gravityScale = 1f;
    }

    //改变动画
    void changeAnimation()
    {
        //变身后滚动
        if (anim.GetBool("isMature"))
            anim.SetBool("isRolling", (input != 0 ? true : false));
        //变身前跑动
        else
            anim.SetBool("isBouncing", (input != 0 ? true : false));
        //是否变身
        if (!anim.GetBool("isMature") && !anim.GetBool("isBouncing") && Input.GetKeyDown(transformKey))
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
