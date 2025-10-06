using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float deletTime = 5.0f;//削除されるまでの時間
    public GameObject boms;//爆発のエフェクト

    void Start()
    {
        //deleteTime秒後に消える
        Destroy(gameObject, deletTime); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //相手がEnemyタグなら相手を削除
            Destroy(other.gameObject);

            //相手がEnemyタグならbomsを生成
            Instantiate(
                boms,//生成したいオブジェクト
                other.transform.position, //相手の位置
                Quaternion.identity
                );
        }
        //最後に自分を削除
        Destroy(gameObject);
    }
}
