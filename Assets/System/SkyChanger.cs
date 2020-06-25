using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    public Material[] Skyboxes;
    private float duration = 0.0f;
    private int numMaterial;
    private float oldTime;
    private bool changeFlag;

    // Start is called before the first frame update
    void Start()
    {
        numMaterial = 0;
        oldTime = Time.time;
        changeFlag = false;
        RenderSettings.skybox = Skyboxes[numMaterial];
//        float lerp = Mathf.PingPong(Time.time, duration) / duration;
 //       rend.material.Lerp(material1, material2, lerp);
  //      RenderSettings.skybox = Skyboxes[_dropdown.value];
   //     RenderSettings.skybox.SetFloat("_Rotation", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - oldTime > 30.0f) {
            oldTime = Time.time;
            changeFlag = true;
        }
        if(changeFlag) {
            var nowMaterial = Skyboxes[numMaterial % Skyboxes.Length];
            var nextMaterial = Skyboxes[(numMaterial + 1) % Skyboxes.Length];
            RenderSettings.skybox.Lerp(nowMaterial, nextMaterial, duration);
            duration += 0.02f;
            if (duration > 1.0f) {
                duration = 0.0f;
                changeFlag = false;
                RenderSettings.skybox = nextMaterial;
                numMaterial += 1;
            }
        }
    }
}
