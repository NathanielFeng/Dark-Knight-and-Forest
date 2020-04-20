using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject boss;
    public GameObject BGM;
    public AudioSource BossBGM;

    private float bgmExitTime = 2.0f;
    private float bgmExit = 0f;
    private bool exitBgmflag = false;
    private bool triggerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffBGM();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !triggerOn)
        {
            boss.GetComponent<BossController>().player = collision.gameObject;
            exitBgmflag = true;
            triggerOn = true;
            BossBGM.Play();
        }
    }

    public void TurnOffBGM()
    {
        AudioSource bgmAudio = BGM.GetComponent<AudioSource>();
        if(BossBGM.volume>=0.54f)
        {
            exitBgmflag = false;
        }
        if (exitBgmflag)
        {
            bgmAudio.volume = Mathf.Lerp(bgmAudio.volume, 0, Time.deltaTime * 5f);
            BossBGM.volume = Mathf.Lerp(BossBGM.volume, 0.55f, Time.deltaTime * 5f);
        }
    }
}
