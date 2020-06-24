using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TouchScript.Gestures;
using System;

public class StageManager : MonoBehaviour
{
    public Camera camera;
    public GameObject tapEffect;
    public GameObject ballPrefab;
    public GameObject wallCubePrefab;
    public GameObject groundCubePrefab;
    private int[,] stageTable;//stageTable(0:ground, 1:wall, 2:ball)

    private GameObject ball;
    struct ObjData {
        public GameObject _gameObject { get; }
        public Vector3 _pos { get; }
        public ObjData(GameObject gameObject, Vector3 pos) { _gameObject = gameObject; _pos = pos; }
    };

    // Start is called before the first frame update
    void Start()
    {
        CreateStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Vector2 worldPos = camera.ScreenToWorldPoint(pos);
        Instantiate(tapEffect, worldPos, Quaternion.identity);
    }

    private void OnFlicked(object sender, EventArgs e) {
        var gesture = sender as FlickGesture;
        ball.GetComponent<BallController>().setFlick(gesture.ScreenFlickVector);
    }

    void CreateStage(int stageNo) {
        //0:ground, 1:wall, 2:ball
        stageTable = new int[10, 10] {
            //0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//0
            { 1, 2, 0, 0, 0, 0, 0, 0, 0, 1 },//1
            { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },//2
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//3
            { 1, 0, 0, 0, 0, 1, 1, 0, 0, 1 },//4
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },//5
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },//6
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1 },//7
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1 },//8
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//9
        };

        // プレハブを元にオブジェクトを生成する
        float cubeWidthHalf = 0.5f;
        float cubeHeightHalf = 0.5f;

        int columnMax = 10;
        int rowMax = 10;
        int columnMaxHalf = columnMax / 2;
        int rowMaxHalf = rowMax / 2;
        float adjustX = (columnMax % 2 == 0) ? 0.5f : 0.0f;
        float adjustY = (rowMax % 2 == 0) ? 0.5f : 0.0f;

        ObjData[] array = new ObjData[columnMax * rowMax + 1];
        int arrayNum = 0;
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
                }
                if (type == 1) {//wall
                    obj = Instantiate(wallCubePrefab, v, Quaternion.identity);
                    obj.transform.parent = this.transform;//子オブジェクトとして登録。
                    array[arrayNum++] = new ObjData(obj, v);
                }
                if (type == 2) {//ball
                    obj = Instantiate(ballPrefab, v, Quaternion.identity);
                    obj.GetComponent<BallController>().ignoredTrigger = true;//接触判定を無効にする。
                    array[arrayNum++] = new ObjData(obj, v);
                    ball = obj;
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
                    LeanTween.move(obj, obj.transform.position + new Vector3(0f, 0.5f, 0f), 1.0f).setEase(LeanTweenType.easeInQuad).setDelay(delay).setOnComplete(()=> {
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
}
