using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //反映対象
    public TextMeshProUGUI odometerText;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI maxScoreText;
    public Slider lifeSlider;

    //データ元
    PlayerController player;
    Shooter shooter;

    //一時記録値
    int currentShotPower;
    int currentPlayerLife;

    //ゲームステータスによる表示・非表示の指定のため取得
    public GameObject odometerPanel;
    public GameObject bulletPanel;
    public GameObject playerLifePanel;
    public GameObject gameOverPanel;
    public GameObject maxScorePanel;

    void Start()
    {
        //データ源を取得して各UIに反映
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<Shooter>();
        maxScoreText.text = PlayerPrefs.GetFloat("Score").ToString("F1");
    }

    void Update()
    {
        if (player == null) return;

        //現記録の保存(毎フレーム)
        odometerText.text = player.gameObject.transform.position.z.ToString("F1");

        //弾の残数表示
        if (currentShotPower != shooter.shotPower)
        {
            currentShotPower = shooter.shotPower;
            bulletText.text = currentShotPower.ToString();
        }

        //HPの表示（変化があった時のみスライダーの値を変える）
        if (currentPlayerLife != player.life)
        {
            currentPlayerLife = player.life;
            lifeSlider.value = currentPlayerLife;
        }

        //ゲームオーバー時に各パネルを非表示
        //ゲームオーバーパネルの表示
        if (GameManager.gameState == GameState.gameover)
        {
            odometerPanel.SetActive(false);
            bulletPanel.SetActive(false);
            playerLifePanel.SetActive(false);
            maxScorePanel.SetActive(false);

            gameOverPanel.SetActive(true);

            //ゲームオーバー時のカーソル表示
            Cursor.lockState = CursorLockMode.None; //ロック解除
            Cursor.visible = true; //カーソルを表示

            //何重にも描画処理しないためにステータス変更ｓ
            GameManager.gameState = GameState.end;
        }
    }
}
