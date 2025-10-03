using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 120; //生成したチップを配置するにあたってのチップの大きさ

    int currentChipIndex; //現在どのチップまで作ったか

    Transform player; //プレイヤーのTransform情報の取得

    public GameObject[] stageChips; //生成すべきオブジェクトを配列に格納

    public int startChipIndex; //自動生成開始インデックス（チップ番号の開始）
    public int preInstantiate; //生成先読み個数(余分に作っておく数)

    //現在生成したオブジェクトの管理用
    public List<GameObject> generatedStageList = new List<GameObject>();

    void Start()
    {
        //transformを獲得
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        if (player != null)
        {
            //キャラクターの位置から現在のステージチップのインデックスを計算。地点をチップの大きさ120で割る。intのため小数点以下は切り捨て
            int charapositionIndex = (int)(player.position.z / StageChipSize);

            //次のステージチップに入ったらステージの更新処理を行う
            if (charapositionIndex + preInstantiate > currentChipIndex)
            {
                UpdateStage(charapositionIndex + preInstantiate);
            }
        }
    }

    //指定のIndexまでのステージチップを生成して管理におく。whileで古いものを削除
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        //指定のステージチップまでを生成
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            //生成したステージチップを管理リストに追加（リストで一番古いものを判別できる）
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;
    }

    //指定のインデックス位置にStageオブジェクトをランダムに生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);//ぴったり並ぶようにチップの長さ分のポジションにする

        GameObject stageObject = Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
        );

        return stageObject;
    }

    //一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0); //リストから消す
        Destroy(oldStage);
    }
}
