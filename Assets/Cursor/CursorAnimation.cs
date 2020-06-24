using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.color(this.gameObject, new Color(Random.value, Random.value, Random.value), 1.0f);
        LeanTween.alpha(this.gameObject, 0, 1.0f);
        LeanTween.scale(this.gameObject, new Vector3(2, 2, 2), 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(LeanTween.isTweening(this.gameObject)) {
            return;
        }
        Destroy(this.gameObject);
    }
}
