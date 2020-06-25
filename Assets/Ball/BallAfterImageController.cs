using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAfterImageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(this.gameObject, 0.15f, 0.0f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.scale(this.gameObject, new Vector3(0.5f, 0.5f, 0.5f), 0.5f).setEase(LeanTweenType.easeInOutSine);
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

