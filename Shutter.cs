//作成シャ　田中　祥太郎
//空間内にいるオブジェクトがプレイヤーフラグでかつマウスボタンが押された時
//オブジェクトが上に動くスクリプトを作りたかったんです。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{

    bool flag;
    [SerializeField] GameObject Player_ray;
    void Start()
    {

    }
    void Update()
    {
        if (transform.position.y > 4 && flag == true)
        {
            this.gameObject.transform.Translate(0, 0.002f, 0);
            Debug.Log("UP");
        }
        if (transform.position.y < 2 && flag == false)
        {
            this.gameObject.transform.Translate(0, -0.002f, 0);
            Debug.Log("Down");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("mousetrue");
            if (Input.GetMouseButton(1))
            {
                flag = true;
                Debug.Log("true");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;
        Debug.Log("false");
    }
}