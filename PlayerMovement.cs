using System.Collections;
using System.Collections.Generic;
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
    Ray ray; //カメラからレーザービームを出す。
    public AudioClip footStepSound;
    public float footStepDelay;
    bool sutaminazero = false;
    private float nextFootstep = 0;
    public int item_count;
    [SerializeField] GameObject[] itemobject;
    [SerializeField] GameObject Slider;


    void Start()
    {
        stam = this.GetComponent<sutaminaBar>();
        cooltime = -5f;
    }

    // Update is called once per frame
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

        //WASDで移動出来るように
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && isGrounded)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {

                //足音
                GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.4f);
                nextFootstep += footStepDelay;
            }
        }

        WalkRun();//走る時の処理
        itemget(itemobject);//アイテム取得の奴


    }

    //Shiftで走行する処理と√2走行対策
    private void WalkRun()
    {
        //左シフトを押している間スピードアップ
        if (((Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && sutamina_hantei() == false) ||
            (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) && sutamina_hantei() == false || (Input.GetKey(KeyCode.LeftShift) &&
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) && sutamina_hantei() == false && ShiftArrowFlag))
        {
            //スタミナがなくなりそうな時の処理
            if (PlayerScript.sutamina >= 0.7)
            {
                speed = 4f;
               // Slider.GetComponent<Image>().color = new Color(53, 255, 0, 255);
            }
            else
            {
                speed = 7f;
            }
        }
        else
        {
            speed = 2f;
        }
      /*しゃがみ機能  if (((Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && sutamina_hantei() == false) ||
    (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) && sutamina_hantei() == false || (Input.GetKey(KeyCode.LeftControl) &&
    (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) && sutamina_hantei() == false && ShiftArrowFlag))
        {
            CharacterController Height =1;
            speed = 1f;
            ShiftArrowFlag = false;
        }*/

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


        /*else if (PlayerScript.sutamina <= 0)
        {
            speed = 1f;

        }*/

        //スタミナがなくなってから3秒経過するまでの処理
        if (nowtime - cooltime <= 3 && (sutaminazero == true))
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
    public void itemget(GameObject[] itemobj = null)
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);//new Ray(transform.position,transform.TransformDirection(Vector3.forward));

            RaycastHit hit;

            //Debug.DrawLine (ray.origin, ray.direction * 10, Color.red);

            //クリックした場所からRayを飛ばす
            if (Physics.Raycast(ray, out hit, 4))
            {

                //当たったオブジェクトのタグがitemだったときの処理
                if (hit.collider.tag == "item")
                {
                    int item_id = hit.collider.GetComponent<item_admin>().item_id;
                    Debug.Log(hit.collider.gameObject.name);

                    itemobj[item_id].SetActive(true);//アイテムオブジェクトをアクティブにする
                    item_count += 1;

                    Destroy(hit.collider.gameObject);
                    //new LeverSwitchesController().OnPushTrue(hit.collider.gameObject.name);
                }
            }

        }

    }

}
