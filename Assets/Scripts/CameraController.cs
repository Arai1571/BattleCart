using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 diff; //ターゲットとの距離の差
    GameObject player; //ターゲットとなるプレイヤー情報

    public float followspeed = 8; //カメラの補間スピード

    //カメラの初期位置
    public Vector3 defualtPos = new Vector3(0, 6, -6);
    public Vector3 defaultRotate = new Vector3(12, 0, 0);

    void Start()
    {
        //カメラを変数で決めた初期位置・角度にする
        transform.position = defualtPos;
        transform.rotation = Quaternion.Euler(defaultRotate); //

        //プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player");

        //プレイヤーとカメラの距離感を記憶しておく
        diff = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        //プレイヤーが見つからなければ何もしない
        if (player==null) return;
            //線形補間を使ってカメラを目的の場所に動かす
            //Lerpメソッド（今の位置、ゴールとすべき位置、割合）
            transform.position = Vector3.Lerp(transform.position, player.transform.position-diff,followspeed*Time.deltaTime);
        
    }
}
