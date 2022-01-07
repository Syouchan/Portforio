using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SELECT : MonoBehaviour
{
    public GameObject[] select_item;//配列
    private float scroll;
    int i = 0;
    private void Start()
    {
        select_item[i].SetActive(true);
    }
    private void Update()
    {
        //マウスホイールの回転の取得※初期値は0.1の為ｘ10
        scroll = Input.GetAxis("Mouse ScrollWheel") * 10;

        //マウスホイールを動かしたとき
        if (scroll != 0)
        {
            //選択アイテムの変更
            if (i < select_item.Length - 1 && i > 0)
            {
                if(scroll > 0) i++;
                else i--;
            }
            else if (i <= 0 && scroll <= 0) i = select_item.Length - 1;
            
            else if(i == 0 && scroll > 0) i += (int)scroll; 
            
            else if(i == select_item.Length -1 && scroll < 0) i -= 1;
            
            else i = 0;
            //セレクターのアクティブ状態の変更
            for(int j= 0; j < select_item.Length; j++)
            {
                select_item[j].SetActive(false);
            }
            select_item[i].SetActive(true);
            scroll = 0;
        }

    }
}
