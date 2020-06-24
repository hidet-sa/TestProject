using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject groundCube;
    // Start is called before the first frame update
    void Start()
    {
        return;
        // プレハブを元にオブジェクトを生成する
        float groundObjectWidthHalf = 0.5f;
        float groundObjectHeightHalf = 0.5f;

        float groundWidth = 10;
        float groundHeight = 10;
        float groundWidthHalf = groundWidth / 2;
        float groundHeightHalf = groundHeight / 2;
        for (float y = -groundHeightHalf; y < groundHeightHalf; y += 1.0f)
        {
            for (float x = -groundWidthHalf; x < groundWidthHalf; x += 1.0f)
            {
                GameObject obj = Instantiate(groundCube, new Vector3(x + groundObjectWidthHalf, -0.5f, y + groundObjectHeightHalf), Quaternion.identity);
                obj.transform.parent = this.transform;//子オブジェクトとして登録。
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
