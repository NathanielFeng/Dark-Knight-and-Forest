using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool getPlayer;
    protected GameObject playerObject;
    public int health;

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

    public void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
