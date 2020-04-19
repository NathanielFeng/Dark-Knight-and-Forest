using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool getPlayer;
    protected GameObject playerObject;
    public int health;

    public float hurtInterval;
    protected float hurtTimeCnt = 0;
    protected bool isHurt = false;
    protected bool isDead = false;

    //角色进入仇恨区域
    public void PlayerInTrigger(GameObject player)
    {
        playerObject = player;
        getPlayer = true;
    }

    //角色进入离开区域
    public void PlayerLeaveTrigger()
    {
        getPlayer = false;
    }

    public void Hurt(int damage)
    {
        isHurt = true;
        hurtTimeCnt = hurtInterval;
        health -= damage;
        if(health <= 0)
        {
            DeadBehavior();
            isDead = true;
        }
        else
        {
            HurtBehavior();
        }
    }

    public void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void HurtIntervalTimmer()
    {
        if (hurtTimeCnt > 0)
            hurtTimeCnt -= Time.deltaTime;
        else
            isHurt = false;
    }

    protected virtual void HurtBehavior()
    {

    }

    protected virtual void DeadBehavior()
    {

    }
}
