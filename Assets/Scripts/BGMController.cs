using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public GameObject globalMusic;
    public string myTag;
    private GameObject myMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        myMusic = GameObject.FindGameObjectWithTag(myTag);
        if (myMusic == null) 
        {
            myMusic = (GameObject)Instantiate(globalMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopMusic()
    {
        Destroy(gameObject);
    }
}
