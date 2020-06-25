using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private Text _text;
    int _minute;
    float _seconds;
    float _oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "00:00";
        _minute = 0;
        _seconds = 0;
        _oldSeconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _seconds += Time.deltaTime;
        if(_seconds > 60.0f) {
            _seconds -= 60.0f;
            _minute++;
            if (_minute >= 60) _minute = 0;
        }
        if((int)_seconds != (int)_oldSeconds) {
            _text.text = _minute.ToString("00") + ":" + ((int)_seconds).ToString("00");
        }
        _oldSeconds = _seconds;
    }
}
