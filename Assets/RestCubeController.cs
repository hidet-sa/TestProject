using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestCubeController : MonoBehaviour
{
    public GameObject stage;
    int _cubeMax;
    int _cubeNum;
    Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "";
        _cubeMax = stage.GetComponent<StageManager>().GetGroundCubeMax();
        _cubeNum = stage.GetComponent<StageManager>().GetChangeGroundCubeNum();
    }

    // Update is called once per frame
    void Update()
    {
        var cubeMax = stage.GetComponent<StageManager>().GetGroundCubeMax();
        var cubeNum = stage.GetComponent<StageManager>().GetChangeGroundCubeNum();

        if(cubeMax != _cubeMax || cubeNum != _cubeNum) {
            _text.text = "Rest:" + cubeNum.ToString() + " / " + cubeMax.ToString();
            _cubeMax = cubeMax;
            _cubeNum = cubeNum;
        }
    }
}
