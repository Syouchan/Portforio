﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_move : MonoBehaviour
{
    public GameObject move;
    public bool isMoveWhenOn = false; //お
    public bool isCanMove = true;//お

    float nowtime;
    float start_time;
    public float speed;
    public float time;

    bool turn = false;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        nowtime = Time.time;
        if (nowtime - start_time > time && turn == false)
        {
            turn = true;
            start_time = Time.time;

        }
        else if (turn)
        {
            transform.Translate(0, 0, speed);
          //move.transform.position = new Vector3(move.transform.position.x, move.transform.position.y, move.transform.position.z + speed);
            if (nowtime - start_time > time)
            {
                start_time = Time.time;
                turn = false;
            }
        }
        else if (nowtime - start_time < time && turn == false)
        {
            transform.Translate(0, 0, -speed);
            //move.transform.position = new Vector3(move.transform.position.x, move.transform.position.y, move.transform.position.z - speed);
        }

    }
    //濃厚接触者
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("col");
            //接触したのがプレイヤーなら移動床の子にする
           // collision.transform.parent = gameObject.transform;
           collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }
    //接触終了
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ok");
            //接触したのがプレイヤーなら子から外す
            //collision.transform.parent = gameObject.transform;

            collision.transform.SetParent(null);
        }
    }
}
