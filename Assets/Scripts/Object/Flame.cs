using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public GameObject detroyEffect;
    private float distance = 0.01f;
    private float lifeTime = 3.0f;
    private float speed = 16.0f;

    void Start()
    {
        Invoke("destroyFlame", lifeTime);
        transform.localScale = new Vector3(1, -1f, 1);
    }

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
        //transform.position += Vector3.down * speed * Time.deltaTime;
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector3.down, distance, layer);
        //if (hitInfo.collider != null)
        //{
        //    if (hitInfo.collider.CompareTag("Player"))
        //        hitInfo.collider.GetComponent<PlayerController>().m_health -= 10;
        //    destroyFlame();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt(10);
            destroyFlame();
        }
    }

    void destroyFlame()
    {
        Instantiate(detroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
