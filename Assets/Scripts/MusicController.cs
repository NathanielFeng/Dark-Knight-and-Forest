using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController: MonoBehaviour
{
    public Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerRB.transform.position.x, playerRB.transform.position.y);
    }
}
