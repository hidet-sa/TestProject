using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearController : MonoBehaviour
{
    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartText(string str) {
        _text.text = str;
        LeanTween.alphaText(_text.rectTransform, 0, 2.0f);
        LeanTween.scale(_text.rectTransform, new Vector3(1.5f, 1.5f, 1.5f), 0.0f);
        LeanTween.scale(_text.rectTransform, new Vector3(3.0f, 3.0f, 3.0f), 2.0f).setDelay(0.5f);
    }

    public void StartText() {
        string[] comment = new string[] {
            "Wow!", "Awesome!", "Fantastic!", "Cool!", "Amazing!", "Wonderful!", "Excellent!", "Great!"
        };
        var num = Random.Range(0, comment.Length - 1);
        StartText(comment[num]);
    }

}
