using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D circleCollider;
    public LayerMask ground;

    public float input;
    public float jumpForce = 8.0f;
    private float faceDirection;
    public float speed = 3.0f;
    public string transformKey;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

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

    void FixedUpdate()
    {
        //移动
        rigid.velocity = new Vector2(input * speed, rigid.velocity.y);
        //朝向
        if (faceDirection != 0)
            transform.localScale = new Vector3(faceDirection, 1, 1);
        //跳起
        if (Input.GetButtonDown("Jump") && circleCollider.IsTouchingLayers(ground) && anim.GetBool("isRolling"))
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }
}
