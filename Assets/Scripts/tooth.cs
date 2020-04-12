using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooth : MonoBehaviour
{
    public float speed;

    private float lifeTime = 2.0f;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("destroyTooth", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }

    void destroyTooth()
    {
        Destroy(gameObject);
    }
}
