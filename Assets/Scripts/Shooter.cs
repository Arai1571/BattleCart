using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; //Bulletのプレハブ情報
    Transform player; //プレイヤーのTransform情報
    GameObject gate; //プレイヤーについているGateオブジェクトの情報
    public float shootSpeed = 100f; //投げた時の力
    public float upSpeed = 8f;　//投げた時の上向きの力

    bool possibleShoot; //ショット可能フラグ

    public int shotPower = 10;
    public int recoverySeconds = 3;

    Camera cam; //カメラ情報の取得

    void Start()
    {
        //時間差でシュート可能にする
        Invoke("ShootEnabled", 0.5f);

        //プレイヤーのTransform情報の取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //プレイヤーについているGateオブジェクト情報の取得
        gate = player.transform.Find("Gate").gameObject;

        //カメラ情報の取得（MainCameraタグがついているカメラ情報は簡単に参照可）
        cam = Camera.main;
    }

    void Update()
    {
        if (GameManager.gameState != GameState.playing) return;

        if (Input.GetMouseButtonDown(0))　//もしも左クリックがおされたら
        {
            if (possibleShoot) Shot(); //フラグがONならショットするメソッド
        }
    }

    //ショット可能にする
    void ShootEnabled()
    {
        possibleShoot = true;
    }

    //ショットメソッド
    void Shot()
    {
        //プレイヤーが消滅していなければ
        if (player == null || shotPower <= 0) return;

        //プレイヤーの位置にBulletを生成
        GameObject obj = Instantiate(bulletPrefab, gate.transform.position, Quaternion.identity);

        //生成したBulletのRigidbodyを取得
        Rigidbody rbody = obj.GetComponent<Rigidbody>();

        //※カメラの角度を考慮した方向を生成
        Vector3 v = new Vector3(
                    cam.transform.forward.x * shootSpeed,
                    cam.transform.forward.y + upSpeed,
                    cam.transform.forward.z * shootSpeed
                    );

        //生成した球のAddForceの力でシュート
        rbody.AddForce(v, ForceMode.Impulse);

        //shotPowerを消費
        ConsumePower();
    }

    //shotPowerを消費
    void ConsumePower()
    {
        shotPower--; //消費
        StartCoroutine(RecoverPower()); //回復コルーチン
    }

    //回復コルーチン
    IEnumerator RecoverPower()
    {
        //RecoverySeconds秒待つ
        yield return new WaitForSeconds(recoverySeconds);
        shotPower++; //１つ回復
    }
}