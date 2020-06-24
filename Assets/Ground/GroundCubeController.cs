using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour
{
    private bool isChangeColor = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor(Color color)
    {
        if(isChangeColor == true)
        {
            return;
        }
        LeanTween.color(this.gameObject, color, 0.5f);
        isChangeColor = true;
    }
}
