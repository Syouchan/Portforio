using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public GameObject move;

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
            transform.Translate(0, speed, 0);
            //move.transform.position = new Vector3(move.transform.position.x, move.transform.position.y, move.transform.position.z + speed);
            if (nowtime - start_time > time)
            {
                start_time = Time.time;
                turn = false;
            }
        }
        else if (nowtime - start_time < time && turn == false)
        {
            transform.Translate(0, -speed, 0);
            //move.transform.position = new Vector3(move.transform.position.x, move.transform.position.y, move.transform.position.z - speed);
        }

    }

}
