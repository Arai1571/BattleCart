using UnityEngine;

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
    }

}
