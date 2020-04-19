using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooth : MonoBehaviour
{
    public float speed;
    private float lifeTime = 5.0f;
    public int damage;
    //public float distance;
    //public LayerMask target;

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("destroyTooth", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
        //射线法
        //但是由于当初角度没算对，手动加了92度，导致判断有误差
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, distance, target);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
                hitInfo.collider.GetComponent<PlayerController>().m_health -= 10;
            destroyTooth();
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().Hurt(damage);
            }
            destroyTooth();
        }
    }

    void destroyTooth()
    {
        Destroy(gameObject);
    }
}
