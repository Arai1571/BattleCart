using UnityEngine;
using UnityEngine.SceneManagement;

//BGMタイプ
public enum BGMType
{
    None,
    Title,
    InGame,
}

//SEタイプ
public enum SEType
{
    Shot,
    Damage,
    Jump,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; //自分自身をStatic変数にしておく。初めて起動したシーンにおけるSceneManagerがずっと変数に残るようにAwakeで指定。
    BGMType playingBGM;
    AudioSource audio;

    public AudioClip titleBGM;
    public AudioClip stageBGM;


    //現シーン取得
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンが切り替わっても破棄されないようにする
        }
        else
        {
            Destroy(gameObject); //2個目以降は自動削除して、最初の1つだけ残す
            return;
        }

        audio = GetComponent<AudioSource>();

    }

    //BGM再生
    public void PlayBgm(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;

            switch (type) //変数で指定する
            {
                case BGMType.Title:
                    audio.clip = titleBGM;
                    audio.Play();
                    break;
                case BGMType.InGame:
                    audio.clip = stageBGM;
                    audio.Play();
                    break;
            }
        }
    }


    //停止メソッド
    public void StopBgm()
    {
        audio.Stop();
        playingBGM = BGMType.None;
    }
}
