using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    public float input;
    public float speed = 3.0f;
    public string transformKey;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        //跑动
        if (input != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
        //变身
        if (Input.GetKeyDown(transformKey))
            anim.SetTrigger("isTransform");
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(input * speed, rigid.velocity.y);
        if (input != 0)
            transform.localScale = new Vector3(input, 1, 1);
    }
}
