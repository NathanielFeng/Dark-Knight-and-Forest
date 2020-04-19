using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    private float burstTime = 5.0f;
    private float dormantTime = 3.0f;
    private float timeCounter;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        anim.SetBool("BurstORFade", true);
        timeCounter = burstTime;
    }

    void Update()
    {
        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            bool status = anim.GetBool("BurstORFade");
            timeCounter = !status ? burstTime : dormantTime;
            anim.SetBool("BurstORFade", !status);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            player.GetComponent<PlayerController>().m_health -= 10;
    }

}
