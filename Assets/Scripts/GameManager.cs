using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    timeover, //ゲームクリア
    playing,
    gameover,
    end
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState; //自作したGameState型のstatic変数
    public static int stagePoints; //ステージの得点

    void Start()
    {
        gameState = GameState.playing;
        stagePoints = 0; //ポイントリセット

        //シーン情報の取得
        Scene currentScene = SceneManager.GetActiveScene();

        //シーン名を取得
        String sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Title":
                SoundManager.instance.PlayBgm(BGMType.Title);
                break;

            case "BaseStage":
                SoundManager.instance.PlayBgm(BGMType.InGame);
                break;
        }

    }

}
