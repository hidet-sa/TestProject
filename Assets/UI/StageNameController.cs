using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNameController : MonoBehaviour
{
    private Text _text;
    public GameObject stage;
    private bool _startFlag;

    // Start is called before the first frame update
    void Start()
    {
        _text = this.GetComponent<Text>();
        _startFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        var stageManager = stage.GetComponent<StageManager>();
        if (stageManager._state == StageManager.State.kStateStart) {
            if (!_startFlag) {
                var stageNo = GameManager.Instance.stageNo;
                StartText("Stage " + stageNo.ToString());
                _startFlag = true;
            }
        } else {
            _startFlag = false;
        }
    }

    public void StartText(string str) {
        _text.text = str;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1.0f);
        _text.rectTransform.LeanScale(new Vector3(1, 1, 1), 0);
        LeanTween.alphaText(_text.rectTransform, 0, 2.0f);
        LeanTween.scale(_text.rectTransform, new Vector3(3, 3, 3), 2.0f).setDelay(1.0f);
    }
}
