# BattleCart
## Battle Cartについて
プレイヤーとなるカートを操作して走行距離の記録を伸ばしていく3Dゲームです。
ステージの奥から敵が迫ってくるのであたらないように避けながら進みます。
  
どんどんスピードが速くなるので後半にいけばいくほど一気に記録が伸びる爽快感と
一瞬でクラッシュしてしまうスリルが癖になるように作りました。

## ゲームプレイ方法
[ゲームのサンプルプレイ](https://arai1571.github.io/BattleCart_web/)

<<<<<<< HEAD
![ゲーム画面](ReadmeImg/battlecart.jpg)
=======
![ゲーム画面](ReadmeImg/battlecart.jpg)

### 操作方法
* Aキー（左キー）：レーンを左に移動
* Dキー（右キー）：レーンを右に移動
* スペースキー：ジャンプ
* マウス操作：視点を回転
* マウス左クリック：シュート
  
### ゲームルール
* 敵とぶつかってPlayerのHPがゼロになったらゲームオーバー
* 敵はシュートされた球で破壊できる
* 弾の残数は限りがある（自動回復）
* 走行距離が記録されるので、最高記録の塗り替えを目指す
* タイトルで記録をリセット可能

## 使用技術
* ゲームエンジン：Unity
* 使用言語：C#
* 使用ツール：VisualStudioCode, Illustrator

## 開発の工夫
* 開発期間：18時間
* 担当範囲：企画〜アセットの導入、プログラミング、デバック仕上げ
* こだわった点：
シュートする時カメラの動きに合わせてカメラの動きに合わせて視点が変わるようにした。
長距離走っても永遠とステージが続くように自動生成されるプログラム。
SEをつけて臨場感が出るようにした。
爽快感とスリルが両立するようにして、繰り返し操作したくなるようなバランス調節を意識した。
* 技術的な挑戦：
カメラの角度を変えると、シュートされる球もカメラの角度に合わせて飛んでいくように調節するRotationの調節が大変でした。

### スクリプトの詳細
* PlayerController.cs
  CharacterControllerコンポーネントのMoveメソッドを用いて自動走行する。
  ダメージを食らった際に一定時間を点滅処理するようにした。
'''C#
 //点滅処理
   void Blinking()
   {
       //その時のゲーム進行時間で正か負かの値を算出
       float val = Mathf.Sin(Time.time * 50);
       //正の周期なら表示
       if (val >= 0) body.SetActive(true);
       //負の周期なら非表示
       else body.SetActive(false);
   }
'''

* CameraRotation.cs
マウスの動きに連動してカメラの視点が変わるように実装した
最大・最小の視野角を決めてそこで視点が止まるようにClampメソッドを活用した
```C#
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
```
* StageGenerator.cs
プレハブに登録したステージオブジェクトが変数に設定した値で自動生成される。
Playerの位置に応じて新しいステージが生成されたのち、古いステージは削除されヒエラルキーを圧迫しないよう工夫した。
```C#
// 指定のインデックス位置にStageオブジェクトをランダムに生成
GameObject GenerateStage(int chipIndex)
{
    int nextStageChip = Random.Range(0, stageChips.Length);
    GameObject stageObject = Instantiate(
        stageChips[nextStageChip],
        new Vector3(0, 0, chipIndex * StageChipSize),
        Quaternion.identity
    );
    return stageObject;
}
```
```C#
    // 一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
```
* Shooter.cs
カメラの向きに合わせて弾プレハブがシュートされる。
残数を決めて、コルーチンで弾数が自動回復するようにした。
```C#
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
```
## 今後の展望
* 自動走行してPlayerを追いかける敵をNaveMeshAgentなどの技術で作る
* オプションメニューを用意して難易度を変えたらカメラの微調整ができるような仕組みを作れるように学習中




>>>>>>> 4d63bf0d2bfbc2324d9aea5f4005dda13da384f1
