using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCountBarController : MonoBehaviour
{
    public GameObject stage;
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        var cubeMax = stage.GetComponent<StageManager>().GetGroundCubeMax();
        var cubeNum = stage.GetComponent<StageManager>().GetChangeGroundCubeNum();
        float value = 0.0f;
        if(cubeMax > 0) {
            value = (float)cubeNum / cubeMax;
        }
        _slider.value = value;
    }
}
