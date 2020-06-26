using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNameController : MonoBehaviour
{
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = this.GetComponent<Text>();
        var stageNo = GameManager.Instance.stageNo;
        StartText("Stage " + stageNo.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartText(string str) {
        _text.text = str;
        LeanTween.alphaText(_text.rectTransform, 0, 2.0f);
        LeanTween.scale(_text.rectTransform, new Vector3(2, 2, 2), 2.0f).setDelay(1.0f);
    }
}
