using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController: MonoBehaviour
{
    public Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerRB.transform.position.x, playerRB.transform.position.y);
    }
}
