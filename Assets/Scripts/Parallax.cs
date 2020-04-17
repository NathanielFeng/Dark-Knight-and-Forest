using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float moveRate;
    public Camera cam;
    private float startPosX;
    public float boundRight;
    public float boundleft;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float modPos = cam.transform.position.x * moveRate;
        if (modPos < boundRight && modPos > boundleft)
        {
            transform.position = new Vector2(startPosX + modPos, transform.position.y);
        }
    }
}
