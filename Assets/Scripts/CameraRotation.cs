using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 3.0f; //マウス感度

    //上下の角度上限
    public float minVerticalAngle = -15.0f; //下を向く角度限界
    public float maxVerticalAngle = 15.0f; //上を向く角度限界

    //左右の角度上限
    public float minHorizontalAngle = -15.0f;
    public float maxHorizontalAngle = 15.0f;

    //プレイ中のカメラの角度
    float verticalRotation = 0;
    float horizontalRotation = 0;

    //プレイ開始時のカメラの左右の角度の基準
    float initialY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //画面中心にカーソルをロック
        Cursor.visible = false; //カーソルを非表示

        Vector3 angles = transform.eulerAngles; //プレイ開始時のカメラの角度
        initialY = angles.y; //あらためてY軸の基準はカメラの値による
        horizontalRotation = 0f; //明確に初期の角度の計算値も0
        verticalRotation = angles.x; //カメラの初期の角度(上下)を入れておく
    }

    void Update()
    {
        //プレイ状態でなければ動かせないようにしておく
        if (GameManager.gameState != GameState.playing) return;

        //マウスの動きを取得しておく
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //その時のマウスの動きに応じた数値（横方向）
        horizontalRotation += mouseX;
        //最大・最小に絞り込みはされる
        horizontalRotation = Mathf.Clamp(horizontalRotation, minHorizontalAngle, maxHorizontalAngle);

        //その時のマウスの動きに応じた数値（縦方向）
        verticalRotation -= mouseY;
        //最大・最小に絞り込みはされる
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        //横の角度の微調整
        //基準としている角度に対してmin～maxの間でのマウス移動の積み重ね
        float yRotation = initialY + horizontalRotation;

        //そのフレームにおけるカメラの角度を最終決定
        transform.rotation = Quaternion.Euler(verticalRotation, yRotation, 0);
    }
}