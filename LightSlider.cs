using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSlider : MonoBehaviour
{
    private float countup = 0.0f;
    public float timeLimit = 5.0f;
    public Slider Lightslider;
    Animator animator;
    private float nowtime;
    public bool startFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        Lightslider = GameObject.Find("Slider").GetComponent<Slider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        nowtime = Time.time;
        countup += Time.deltaTime; //ŽžŠÔƒJƒEƒ“ƒg
        if(startFlag)
        {
            timeLimit -= Time.deltaTime;
        }
        if(timeLimit <= 0.0)
        {
            timeLimit = 1.0f;
        }
    }
}
