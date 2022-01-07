/*
  プログラム名：enemy_controller
  プログラム概要：オブジェクトがチェックポイントを周回し、プレイヤーを見つけると追跡する
  作成者：浅野　龍太郎
  作成日：2021/9/19

  Nav Mesh Agentおすすめ
  Speed:5
  Anglar Speed:120
  Acceleration:100
  Auto Brakingのチェックを外す

  使用方法

    1.適用したいオブジェクトにこのスクリプトをアタッチ

    2.Collider2つ(isTriggerにチェックが入ったものと入っていないもの)とRigidbodyをセット

    3.checkpointタグがついたオブジェクトを用意し、通過させたい場所に接地  ※2箇所以上接地しないと周回しない

    4.プレイヤーオブジェクトのタグをPlayerに変更


    動くなｯ！
    ∧__∧　ファボを打ち込むぞ
    ( ｀Д´ ）
    (っ▄︻▇〓┳═☆
    /　　 )
    ( /￣∪

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoint;//チェックポイント用オブジェクト
    private int nextIndex = 0; //次の目的地のインデックス
    private UnityEngine.AI.NavMeshAgent navMeshAgent;//AIのコンポーネント
    private bool playerflag = false;//プレイヤー追跡中フラグ
    Ray ray;//プレイヤーに向かって常に飛ばし続けるRay
    private GameObject Players;//プレイヤーオブジェクト
    [SerializeField] int serch_distance = 20;//索敵範囲
    public float speed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();//AIのコンポーネントを取得
        checkpoint = GameObject.FindGameObjectsWithTag("checkpoint");//checkpointタグのついたオブジェクトを全て取得
        Players = GameObject.FindWithTag("Player");

        navMeshAgent.destination = checkpoint[nextIndex].transform.position;//最初の目的地を設定

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー探索に関する処理------------------------------------------

        ray = new Ray(transform.position, (Players.transform.position - transform.position).normalized);//Rayをプレイヤーの方向に飛ばす
                                                                                                        //Debug.DrawLine (ray.origin, ray.direction * Vector3.Distance(transform.position,Players.transform.position), Color.red);//ray可視化用  ※残すべし
        RaycastHit hit;

        //Rayが何かに当たった時の処理
        if (Physics.Raycast(ray, out hit, serch_distance))
        {
            //当たったオブジェクトがプレイヤーだった時の処理
            if (hit.transform.gameObject == Players)
            {
                Debug.Log("追跡中");
                speed = 5f;
                playerflag = true;//追跡中フラグを有効化
            }
        }
        //プレイヤーオブジェクトを追跡中の時の処理
        if (playerflag)
        {
            //チェックポイントをプレイヤーに変更して追跡を開始
            checkpoint = GameObject.FindGameObjectsWithTag("Player");
            navMeshAgent.destination = Players.transform.position;
        }

        //-----------------------------------------------------------------
        //チェックポイントの周回に関する処理----------------------------------


        //プレイヤー追跡フラグがfalseの時の処理
        if (playerflag == false)
        {
            //チェックポイントに到達した時の処理
            if (Vector3.Distance(transform.position, checkpoint[nextIndex].transform.position) <= 2)
            {
                nextIndex++;

                //すべてのチェックポイントを回ったら周回をリセット
                if (nextIndex >= checkpoint.Length)
                {
                    nextIndex = 0;
                }
                checkpoint = GameObject.FindGameObjectsWithTag("checkpoint");
                navMeshAgent.destination = checkpoint[nextIndex].transform.position;// インデックスに応じた目的地を設定する
            }
        }


        //-----------------------------------------------------------------

    }

    //他オブジェクトに衝突したときに呼び出されるメソッド
    void OnCollisionEnter(Collision collision)
    {

        //衝突対象がプレイヤーだった時の処理
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver_Scene");
            Debug.Log("ゲームオーバー");
        }

    }

    //プレイヤーが探索エリアから離れたときの処理
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerflag = false;
            checkpoint = GameObject.FindGameObjectsWithTag("checkpoint");
            navMeshAgent.destination = checkpoint[nextIndex].transform.position;// インデックスに応じた目的地を設定する
        }
    }


}
