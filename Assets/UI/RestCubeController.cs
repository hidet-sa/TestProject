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
        _cubeMax = stage.GetComponent<StageManager>().GetGroundCubeMax();
        _cubeNum = stage.GetComponent<StageManager>().GetChangeGroundCubeNum();
        _text.text = "Cube: " + _cubeNum.ToString() + " / " + _cubeMax.ToString();
        LeanTween.alphaText(_text.rectTransform, 0.0f, 0.0f);
        LeanTween.alphaText(_text.rectTransform, 1.0f, 1.0f).setDelay(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var cubeMax = stage.GetComponent<StageManager>().GetGroundCubeMax();
        var cubeNum = stage.GetComponent<StageManager>().GetChangeGroundCubeNum();

        if(cubeMax != _cubeMax || cubeNum != _cubeNum) {
            _text.text = "Cube :" + cubeNum.ToString() + " / " + cubeMax.ToString();
            _cubeMax = cubeMax;
            _cubeNum = cubeNum;
        }
    }
}
