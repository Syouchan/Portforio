//作成日：2021/09/28
//作成者：田中祥太郎
//スクリプト概要
//インスペクター上でセットしたカメラをアクティブ非アクティブに変える処理
//スペースを押すとアクティブ状態になり、押していない間、非アクティブになる。
//プレイヤーの背後を写すためのスクリプトである。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subcamera : MonoBehaviour
{
    public GameObject SubCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SubCamera.SetActive(true);
        }
        else
        {
            SubCamera.SetActive(false);
        }
    }
}
