using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TouchScript.Gestures;
using System;

public class StageManager : MonoBehaviour {

    public enum State {
        kStateNone,
        kStateStart,
        kStatePlay,
        kStateEnd,
    };

    public Camera camera;
    public GameObject tapEffect;
    public GameObject ballPrefab;
    public GameObject wallCubePrefab;
    public GameObject groundCubePrefab;
    private int[,] stageTable;//stageTable(0:ground, 1:wall, 2:ball)

    private GameObject[] ground;
    private GameObject[] wall;
    private GameObject ball;
    struct ObjData {
        public GameObject _gameObject { get; }
        public Vector3 _pos { get; }
        public ObjData(GameObject gameObject, Vector3 pos) { _gameObject = gameObject; _pos = pos; }
    };

    private bool _clearEffectFlag;
    public State _state = State.kStateNone;

    // Start is called before the first frame update
    void Start() {
        _state = State.kStateStart;
    }

    // Update is called once per frame
    void Update() {

        switch (_state) {
        case State.kStateNone:
            break;
        case State.kStateStart:
            var stageNo = GameManager.Instance.stageNo;
            var stageData = GameManager.Instance.LoadStage(stageNo);
            CreateStage(stageData.getTable());
            Camera.main.GetComponent<CameraController>().StartCamera(stageNo % 2 == 0);
            _state = State.kStatePlay;
            break;
        case State.kStatePlay:
            if(LeanTween.isTweening()) {
                break;
            }
            //クリアチェック。
            if (GetGroundCubeMax() == GetChangeGroundCubeNum()) {
                StartClearEffect();
                GameManager.Instance.stageNo += 1;
                if (GameManager.Instance.stageNo > 10) {
                    GameManager.Instance.stageNo = 1;
                }
                GameManager.Instance.SavePlayData();
                _state = State.kStateEnd;
            }
            break;
        case State.kStateEnd:
            if(LeanTween.isTweening()) {
                break;
            }
            RemoveState();
            _state = State.kStateStart;
            break;
        }

    }

    private void OnEnable() {
        GetComponent<TapGesture>().Tapped += OnTapped;
        GetComponent<FlickGesture>().Flicked += OnFlicked;
    }

    private void OnDisable() {
        GetComponent<TapGesture>().Tapped -= OnTapped;
        GetComponent<FlickGesture>().Flicked -= OnFlicked;
    }

    private void OnTapped(object sender, EventArgs e) {
        var gesture = sender as TapGesture;
        Vector3 pos = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, 1);
        if(float.IsNaN(pos.x) || float.IsNaN(pos.y)) {
            return;
        }
        Vector2 worldPos = camera.ScreenToWorldPoint(pos);
        Instantiate(tapEffect, worldPos, Quaternion.identity);
    }

    private void OnFlicked(object sender, EventArgs e) {
        var gesture = sender as FlickGesture;
        ball.GetComponent<BallController>().setFlick(gesture.ScreenFlickVector);
    }

    //地面ブロック数を取得する。
    public int GetGroundCubeMax() {
        int cubeNum = 0;
        if (stageTable == null) {
            return cubeNum;
        }
        for(int i = 0; i < stageTable.GetLength(0); i++) {
            for(int j = 0; j < stageTable.GetLength(1); j++) {
                var type = stageTable[i, j];
                if (type == 0 || type == 2) { //グラウンド or ボール
                    cubeNum++;
                }
            }
        }
        return cubeNum;
    }
    
    //地面キューブで変化した数を取得する。
    public int GetChangeGroundCubeNum() {
        int cubeNum = 0;
        if(ground == null) {
            return cubeNum;
        }
        for(int i = 0; i < ground.Length; i++) {
            var p = ground[i];
            if (p != null && p.GetComponent<GroundCubeController>().isChangeColor) {
                cubeNum++;
            }
        }
        return cubeNum;
    }

    public int FindMoveX(Vector2Int v, float direct) {
        int moveX = 0;
        int dir = (direct > 0) ? 1 : -1;
        var type = stageTable[v.y, v.x];
        if(type == 1) {
            return moveX;
        }
        while(true) { 
            if((v.x + dir < 0) || (v.x + dir >= stageTable.GetLength(1))) {
                break;
            }
            type = stageTable[v.y, v.x + dir];
            if(type == 1) {
                break;
            }
            v.x += dir;
            moveX += dir;
        }
        return moveX;
    }

    public int FindMoveY(Vector2Int v, float direct) {
        int moveY = 0;
        int dir = (direct > 0) ? 1 : -1;
        var type = stageTable[v.y, v.x];
        if(type == 1) {
            return moveY;
        }
        while(true) {
            if((v.y + dir < 0) || (v.y + dir >= stageTable.GetLength(0))) {
                break;
            }
            type = stageTable[v.y + dir, v.x];
            if(type == 1) {
                break;
            }
            v.y += dir;
            moveY += dir;
        }
        return moveY;
    }

    void CreateStage(int[,] table) {

        stageTable = table;

        // プレハブを元にオブジェクトを生成する
        int columnMax = stageTable.GetLength(0);
        int rowMax = stageTable.GetLength(1);
        int columnMaxHalf = columnMax / 2;
        int rowMaxHalf = rowMax / 2;
        float adjustX = (columnMax % 2 == 0) ? 0.5f : 0.0f;
        float adjustY = (rowMax % 2 == 0) ? 0.5f : 0.0f;
        int groundCubeMax = GetGroundCubeMax();
        ground = new GameObject[groundCubeMax];
        wall = new GameObject[stageTable.Length - groundCubeMax];

        ObjData[] array = new ObjData[columnMax * rowMax + 1];
        int arrayNum = 0;
        int groundCubeNum = 0;
        int wallCubeNum = 0;
        for (int column = 0; column < columnMax; column++) {
            for(int row = 0; row < rowMax; row++) {
                Vector3 v = new Vector3(
                    -rowMaxHalf + row + adjustX,
                    0.0f,
                    columnMaxHalf - column + adjustY
                );
                int type = stageTable[column, row];
                GameObject obj = null;
                if (type == 0 || type == 2) {//ground
                    obj = Instantiate(groundCubePrefab, v, Quaternion.identity);
                    obj.transform.parent = this.transform;//子オブジェクトとして登録。
                    array[arrayNum++] = new ObjData(obj, v);
                    obj.GetComponent<GroundCubeController>().stagePos = new Vector2Int(column, row);
                    ground.SetValue(obj, groundCubeNum++);
                }
                if (type == 1) {//wall
                    obj = Instantiate(wallCubePrefab, v, Quaternion.identity);
                    obj.transform.parent = this.transform;//子オブジェクトとして登録。
                    array[arrayNum++] = new ObjData(obj, v);
                    wall.SetValue(obj, wallCubeNum++);
                }
                if (type == 2) {//ball
                    obj = Instantiate(ballPrefab, v, Quaternion.identity);
                    var ballController = obj.GetComponent<BallController>();
                    ballController.ignoredTrigger = true;//接触判定を無効にする。
                    ballController.stage = this.gameObject;
                    v.y += 1.0f;
                    array[arrayNum++] = new ObjData(obj, v);

                    ball = obj;
                    ball.GetComponent<BallController>().stagePos = new Vector2Int(column, row);
                }
            }
        }

        //演出
        float delayCount = 0f;
        for (int i = 0; i < array.Length; i++) {
            GameObject obj = array[i]._gameObject;
            obj.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10), UnityEngine.Random.Range(-10, 10));
            float delay = (float)delayCount / array.Length;
            LeanTween.move(obj, array[i]._pos, 1.0f).setEase(LeanTweenType.easeInSine).setOnComplete(()=>{
                if (obj.CompareTag("Wall")) {
                    LeanTween.move(obj, obj.transform.position + new Vector3(0f, 0.5f, 0f), 1.0f).setEase(LeanTweenType.easeInQuad).setDelay(delay);
                } else if (obj.CompareTag("Ball")) {
                    LeanTween.move(obj, obj.transform.position + new Vector3(0f, -0.5f, 0f), 1.0f).setEase(LeanTweenType.easeInQuad).setDelay(delay).setOnComplete(()=> {
                        //接触判定無効を有効にする。
                        obj.GetComponent<BallController>().ignoredTrigger = false;
                    });
                } else {
                    LeanTween.move(obj, obj.transform.position + new Vector3(0f, -0.5f, 0), 1.0f).setEase(LeanTweenType.easeInQuad).setDelay(delay);
                }
            });
            delayCount++;
        }
    }

    void StartClearEffect() {
        //wall
        for(int i = 0; i < wall.Length; i++) {
            LeanTween.move(wall[i], wall[i].transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10), UnityEngine.Random.Range(-10, 10)), 2.0f);
        }
        //ground
        for (int i = 0; i < ground.Length; i++) {
            LeanTween.move(ground[i], ground[i].transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10), UnityEngine.Random.Range(-10, 10)), 2.0f);
        }
        //ball
        LeanTween.move(ball, ball.transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10), UnityEngine.Random.Range(-10, 10)), 2.0f);
    }

    void RemoveState() {
        //wall
        for (int i = 0; i < wall.Length; i++) {
            Destroy(wall[i]);
        }
        //ground
        for (int i = 0; i < ground.Length; i++) {
            Destroy(ground[i]);
        }
        //ball
        Destroy(ball);
    }


}
