using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[DefaultExecutionOrder(-1)]//優先的にこちらを処理。
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stageNo;

    void Awake() {
        if (null != Instance) {
            // 既に存在しているなら削除
            Destroy(gameObject);
        } else {
            // 存在していないなら指定する
            Instance = this;
        }

        // シーン遷移では破棄させない
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayData();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartScene() {
        var nowScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(nowScene.name);
    }

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width - 80, Screen.height - 30, 60, 20), "Restart")) {
            RestartScene();
        }
    }

    public void SavePlayData() {
        PlayerPrefs.SetInt("StageNo", stageNo);
    }

    public void LoadPlayData() {
        stageNo = 1;// PlayerPrefs.GetInt("StageNo", 1);
    }


    [Serializable]
    public class StageData {
        public int stageNo;//ステージ番号
        public int column;//列数（縦）
        public int row;//行数（横）
        public int[] table;//テーブル

        public int[,] getTable() {
            int[,] table2 = new int[column, row];
            for(int i = 0; i < column; i++) {
                for(int j = 0; j < row; j++) {
                    table2[i, j] = table[i * row + j];
                }
            }
            return table2;
        }
    }

    public StageData LoadStage(int stageNo) {
        string fileName = "stage" + stageNo.ToString("00");
        string data = Resources.Load<TextAsset>(fileName).ToString();
        return JsonUtility.FromJson<StageData>(data);
    }
}
