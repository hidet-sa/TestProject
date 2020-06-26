using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    public Material[] Skyboxes;
    private int numMaterial;
    private bool changeFlag;

    // Start is called before the first frame update
    void Start()
    {
        numMaterial = 0;
        changeFlag = false;
        RenderSettings.skybox = Skyboxes[numMaterial];
    }

    // Update is called once per frame
    void Update()
    {
        var stageManager = this.GetComponent<StageManager>();
        if (stageManager._state == StageManager.State.kStateStart) {
            if (changeFlag == false) {
                var nextMaterial = Skyboxes[(numMaterial + 1) % Skyboxes.Length];
                RenderSettings.skybox = nextMaterial;
                numMaterial += 1;
                changeFlag = true;
            }
        } else {
            changeFlag = false;
        }
    }
}
