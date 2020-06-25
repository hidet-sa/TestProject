using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(20f, 3f, 0f);
        this.gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        LeanTween.move(this.gameObject, new Vector3(0f, 18f, -2.0f), 1f).setEase(LeanTweenType.easeInQuad);
        LeanTween.rotate(this.gameObject, new Vector3(80f, 0f, 0f), 1f).setEase(LeanTweenType.easeInQuad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
