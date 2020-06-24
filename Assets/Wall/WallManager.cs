using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wallCube;


    // Start is called before the first frame update
    void Start()
    {
        return;
        int[,] wallTable = new int[10, 10] {
            //0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//0
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//1
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//2
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },//3
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },//4
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },//5
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },//6
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1 },//7
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1 },//8
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//9
        };

        // プレハブを元にオブジェクトを生成する
        float wallObjectWidthHalf = 0.5f;
        float wallObjectHeightHalf = 0.5f;

        float wallWidth = 10;
        float wallHeight = 10;
        float wallWidthHalf = wallWidth / 2;
        float wallHeightHalf = wallHeight / 2;
        float delayCount = 0f;

        int wallTableX = 0;
        int wallTableY = 0;
        for (float y = -wallHeightHalf; y < wallHeightHalf; y += 1.0f)
        {
            wallTableX = 0;
            for (float x = -wallWidthHalf; x < wallWidthHalf; x += 1.0f)
            {
                int wallTableFlag = wallTable[wallTableY, wallTableX];
                if (wallTableFlag == 1)
                {
                    //Random.Range(-5, 5);
                    //GameObject obj = Instantiate(wallCube, new Vector3(x + wallObjectWidthHalf, 2.5f, y + wallObjectHeightHalf), Quaternion.identity);
                    GameObject obj = Instantiate(wallCube, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
                    obj.transform.parent = this.transform;//子オブジェクトとして登録。
                    LeanTween.move(obj, new Vector3(x + wallObjectWidthHalf, 1.5f, y + wallObjectHeightHalf), 1.0f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        //    move(obj, obj.transform.position + new Vector3(0f, -2f, 0f), 1.0f).setEase(LeanTweenType.easeInQuad).setDelay(delayCount / (wallWidth * wallHeight));
                        LeanTween.move(obj, obj.transform.position + new Vector3(0f, -1f, 0f), 1.0f).setEase(LeanTweenType.easeInQuad);//.setDelay(delayCount / (wallWidth * wallHeight));
                    });
                }
                delayCount += 1.0f;
                wallTableX++;
            }
            wallTableY++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
