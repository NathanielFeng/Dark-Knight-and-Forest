using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [Header("Attributes")]
    public float health = 100;  //生命值100
    public float attackRange;   //攻击范围
    public float minDistance;   //和player保持的最小距离
    public float flySpeed;
    public float timeBtwShot;   //攻击间隔
    private float timeCounterBtwShot;   //攻击间隔计时

    [Header("Components")]
    public Transform player;   //获取黑骑士的坐标
    private Animator anim;
    public GameObject tooth;
    public Transform shotPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange) 
        {
            anim.SetBool("isAttacking", true);
            //朝player方向移动
            if (Vector2.Distance(transform.position, player.position) > minDistance)
                    transform.Translate((player.position - transform.position) * flySpeed * Time.deltaTime);
            //攻击
            if (timeCounterBtwShot <= 0)
            {
                Vector3 difference = player.transform.position - transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 92.0f;   //搞不好了，只能手动加个92度
                Instantiate(tooth, shotPoint.position, Quaternion.Euler(0f,0f,rotZ));
                timeCounterBtwShot = timeBtwShot;
            }
            else
                timeCounterBtwShot -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }


    }
}
