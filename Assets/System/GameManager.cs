using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stageNo;
    public int coinNum;

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
        stageNo = 0;
        coinNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartScene() {
        var nowScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(nowScene.name);
    }
}
