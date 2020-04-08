using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    public float input;
    private float faceDirection;
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
        input = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

        //变身
        if(Input.GetKeyDown(transformKey))
            anim.SetBool("transform", true);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(input * speed, rigid.velocity.y);
        if(faceDirection != 0)
            transform.localScale = new Vector3(faceDirection, 1, 1);
    }
}
