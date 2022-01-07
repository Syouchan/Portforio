//作成日忘れた
//作成者　田中祥太郎　浅野龍太郎
//スクリプト概要
//WASDで移動できる。speedは2f。
//左シフトを押しながらWASDのどれかを押すとspeedが7fの状態で動ける。
//しかしその状態で動き続けるとペナルティが発生するようになっている。
//内容は用意しているスライダーが８割に到達すると7fから5fまで減速するようになっており、
//スライダーの一番端に到達するまで動き続けてしまうと、３秒間左シフトの押下が不可になる（走れない）。
//左コントロールを押した状態でWASDを押すとspeedが1fの状態で移動できる。
//√２走法はできないようにスクリプトで組んでいるため、斜め走法を行ってもスピードは変わらない。
//プレイヤーのカメラからレーザーを放ち、一定距離のレーザーに触れているアイテムオブジェクトに向かって
//左クリックを押すとアイテムの取得までは完成済み。しかしアイテムの使用やUIの設定は終わっていない。


using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 3f;//走る速度
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    sutaminaBar stam;
    private bool ShiftArrowFlag = true;//Shift押下可能フラグ
    private float cooltime;//スタミナがなくなった瞬間の時間
    private float nowtime;//現在の時間
    //Ray Player_ray; //カメラからレーザービームを出す。

    public float footStepDelay;
    bool sutaminazero = false;
    private float nextFootstep = 0;
    public int item_count;
    public int key_item = 1;//ゴールに必要なキーアイテムの総数
    [SerializeField] bool GoalFlag = false;//ゴール可能フラグ
    [SerializeField] GameObject[] itemobject;
    [SerializeField] GameObject Slider;
    [SerializeField] GameObject text;


    //public GameObject SubCamera;


    void Start()
    {
        stam = this.GetComponent<sutaminaBar>();
        cooltime = -5f;
    }

    void Update()
    {
        nowtime = Time.time;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //WASD入力判定　※矢印キーも入力判定に入ってしまう
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;
        controller.Move(motion * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        WalkRun();//走る時の処理
        //itemget(itemobject);//アイテム取得の奴
        sutamina_hantei();
    }

    //Shiftで走行する処理と√2走行対策
    private void WalkRun()
    {
        if(Input.GetKey(KeyCode.RightArrow)&& Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            speed = 0f;
        }
        //左シフトを押している間スピードアップ
        if (((Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && sutamina_hantei() == false) ||
            (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) && sutamina_hantei() == false || (Input.GetKey(KeyCode.LeftShift) &&
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) && sutamina_hantei() == false && ShiftArrowFlag))
        {
            //スタミナがなくなりそうな時の処理
            if (PlayerScript.sutamina >= 0.8)
            {
                speed = 5f;
                
                //Slider.GetComponent<Image>().color = new Color(53, 255, 0, 255);
            }
            else
            {
                speed = 7f;
                //Slider.GetComponent<Image>().color = new Color(0, 255, 0, 255);
            }
        }
        else
        {
            speed = 2f;
        }
        if (((Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && sutamina_hantei() == false) ||
    (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) && sutamina_hantei() == false || (Input.GetKey(KeyCode.LeftControl) &&
    (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) && sutamina_hantei() == false && ShiftArrowFlag))
        {
            //CharacterController Height =1f;
            speed = 1f;
            ShiftArrowFlag = false;
        }

            //ルート２走法対策
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) ||
            (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)))
        {
            speed *= 1 / Mathf.Sqrt(2); //Mathf.Sqrt(2)=√2の意味
        }
    }

    private bool sutamina_hantei()
    {
        //スタミナがなくなった時の処理
        if (PlayerScript.sutamina >= 1)
        {
            cooltime = Time.time;
            sutaminazero = true;
            return true;
        }
        if(PlayerScript.sutamina >= 0.8)//なんだろう無視するの辞めて貰って良いですか。
        {
            Slider.GetComponent<Image>().color = new Color(53, 255, 0, 255);
        }
        else
        {
            Slider.GetComponent<Image>().color = new Color(0, 255, 0, 255);
        }
        //スタミナがなくなってから3秒経過するまでの処理
        if (nowtime - cooltime <= 5 && (sutaminazero == true))
        {
            Slider.GetComponent<Image>().color = new Color(255, 0, 0, 255);
            ShiftArrowFlag = false;//Shiftを押下不可にする
            return true;
        }
        else
        {
            Slider.GetComponent<Image>().color = new Color(0, 255, 0, 255);
            sutaminazero = false;
            ShiftArrowFlag = true;//Shiftを押下可能にする
        }
     
        return false;
    }
    //public void itemget(GameObject[] itemobj = null)
    //{

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //Player_ray = Camera.main.ScreenPointToRay(Input.mousePosition);//new Ray(transform.position,transform.TransformDirection(Vector3.forward));

    //        //RaycastHit hit;

    //        //Debug.DrawLine (Player_ray.origin, Player_ray.direction * 10, Color.red);

    //        //クリックした場所からRayを飛ばす
    //        if (Physics.Raycast(Player_ray, out hit, 4))
    //        {

    //            //当たったオブジェクトのタグがitemだったときの処理
    //            if (hit.collider.tag == "item")
    //            {
    //                //Debug.DrawLine(Player_ray.origin, Player_ray.direction * 10, Color.red);
    //                text.SetActive(true);
    //                int item_id = hit.collider.GetComponent<item_admin>().item_id;
    //                //Debug.Log(hit.collider.gameObject.name);

                    
    //                itemobj[item_id].SetActive(true);//アイテムオブジェクトをアクティブにする
    //                item_count += 1;

    //                //拾ったアイテムが鍵アイテムだった時の処理
    //                if(item_id==1)
    //                {
    //                    key_item -= 1;//必要な残りキーアイテムの総数を減らす
    //                    if (key_item<=0)
    //                    {
    //                        GoalFlag = true;
    //                    }
    //                }


    //                Destroy(hit.collider.gameObject);
    //                //new LeverSwitchesController().OnPushTrue(hit.collider.gameObject.name);
    //            }

    //            //当たったオブジェクトのタグがgoalだった時の処理
    //            if (hit.collider.tag == "goal")
    //            {
                    
    //                if (GoalFlag == false)
    //                {
    //                    Debug.Log("ゴールに必要なアイテムが不足しています");
    //                }
    //                else
    //                {
    //                    SceneManager.LoadScene("Clear_Scene");
    //                }
    //            }
    //            if(hit.collider.tag == "Gate")
    //            {
    //                text.SetActive(true);
    //            }
    //            else
    //            {
    //                text.SetActive(false);
    //            }
    //        }
    //    }
    //    Player_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //}
    //足音を鳴らす
    //void ashioto()
    //{
    //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
    //    {
    //        aud.Play();
    //    }

    //    else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
    //    {
    //        aud.Stop();
    //    }
    //}





    //void runashioto()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) ||
    //        Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A) ||
    //        Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
    //    {
    //        runaud.Play();
    //    }

    //    else if (Input.GetKeyUp(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.W) ||
    //        Input.GetKeyUp(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.A) ||
    //        Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
    //    {
    //        runaud.Stop();
    //    }
    //}
}
