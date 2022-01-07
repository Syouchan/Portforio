//作成社　田中　
//文字通りスタミナ用のスクリプトである。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sutaminaBar : MonoBehaviour
{
    public float speed = 8.0f;
    public Slider slider;
    Animator animator;
    //UIScript uiscript;
    public float timeToEnableInputs;

    private bool ShiftArrowFlag = true;//Shift押下可能フラグ
    private float cooltime;//スタミナがなくなった瞬間の時間
    private float nowtime;//現在の時間
    

    // Start is called before the first frame update
    void Start()
    {
        //slider = GameObject.Find("Slider").GetComponent<Slider>();
        animator = GetComponent<Animator>();
        //uiscript = GameObject.Find("Canvas").GetComponent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {

        nowtime = Time.time;//現在の経過時間を取得
        

        //Shift+Wを押下中で、Shiftを押下不可にする押下可能フラグがtrueの時の処理
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && this.ShiftArrowFlag == true)
        {
            PlayerScript.sutamina = PlayerScript.sutamina + (Time.deltaTime / 10);//デフォルト10
        }
        else
        {
            PlayerScript.sutamina = PlayerScript.sutamina - (Time.deltaTime / 15);//スタミナが回復する
        }

        

        //スタミナがなくなった時の処理
        if (PlayerScript.sutamina >= 1)
        {
                   
            this.ShiftArrowFlag = false;//Shiftを押下不可にする
            cooltime = Time.time;//スタミナがなくなった瞬間の時間を取得
        }

        //スタミナがなくなって5秒以上経過した時の処理
        if (nowtime - cooltime >= 5 && this.ShiftArrowFlag == false)
        {
            speed = 1f;
            this.ShiftArrowFlag = true;//Shiftを押下可能にする
        }


        if (Time.time >= this.timeToEnableInputs)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                var stick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
        if (PlayerScript.sutamina < 0)
        {
            speed = 0;
            //animator.SetBool("DEAD", true);
            PlayerScript.sutamina = 0;
        }
        if (PlayerScript.sutamina > 1)
        {
            PlayerScript.sutamina = 1;
        }


        //スタミナとvalueをつなげる
        //スタミナをスライダーに反映させる処理
        slider.value = PlayerScript.sutamina;
    }

}