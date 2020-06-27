using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInpactController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var material = gameObject.GetComponent<Material>();
        //material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        LeanTween.alpha(this.gameObject, 0.25f, 0.2f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.scale(this.gameObject, new Vector3(2.0f, 2.0f, 2.0f), 0.2f).setEase(LeanTweenType.easeInOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        if (LeanTween.isTweening(this.gameObject)) {
            return;
        }
        Destroy(this.gameObject);
    }
}
