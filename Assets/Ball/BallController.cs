using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TouchScript.Gestures.TransformGestures;

public class BallController : MonoBehaviour
{
    private Vector2 flickVector;
    private bool flickFlag;
    private int counter = 0;
    public GameObject ballAfterImage = null;
    private Vector3 oldPosition;
    public bool ignoredTrigger = false;
    public GameObject effect = null;
    public GameObject stage = null;
    public Vector2Int stagePos;
    float speed = 0.04f;
    private string changeColor = "#64A70B";

    // Start is called before the first frame update
    void Start()
    {
        flickFlag = false;
        flickVector = new Vector2(0, 0);
        oldPosition = this.gameObject.transform.position;
        //stage = GameObject.Find("stage");
    }

    // Update is called once per frame
    void Update()
    {
        //フリック
        if (flickFlag == true) {
            flickFlag = false;
            if (flickVector.x != 0f) {
                //this.gameObject.transform.position += new Vector3(flickVector.x, 0f, 0);
                //Debug.Log("FlickX:" + flickVector.x.ToString());
                var moveX = stage.GetComponent<StageManager>().FindMoveX(stagePos, flickVector.x);
                stagePos.x += moveX;
                //Debug.Log("X:" + moveX.ToString() + ":" + flickVector.x.ToString());
                LeanTween.moveX(this.gameObject, this.gameObject.transform.position.x + moveX, Math.Abs(moveX * speed));
            }
            if (flickVector.y != 0f) {
                //this.gameObject.transform.position += new Vector3(0, 0, flickVector.y);
                //Debug.Log("FlickY:" + flickVector.y.ToString());
                var moveZ = stage.GetComponent<StageManager>().FindMoveY(stagePos, -flickVector.y);
                stagePos.y += moveZ;
                //Debug.Log("Y:" + moveZ.ToString() + ":" + flickVector.y.ToString());
                LeanTween.moveZ(this.gameObject, this.gameObject.transform.position.z - moveZ, Math.Abs(moveZ * speed));
            }
        }

        //残像処理。
        if (oldPosition != this.gameObject.transform.position) {
            if (counter % 2 == 0) {
                if (ballAfterImage != null) {
                    Instantiate(ballAfterImage, this.gameObject.transform.position, Quaternion.identity);
                }
            }
        }
        oldPosition = this.gameObject.transform.position;

        //壁に当たった時のエフェクト。
        if(counter % 90 == 0) {
            var obj = Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);
        }
        counter++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ignoredTrigger == true) {
            return;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("Collided Wall!");
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Collided Ground");
            Color color = Color.white;
            ColorUtility.TryParseHtmlString(changeColor, out color);
            other.GetComponent<GroundCubeController>().changeColor(color);
        }
    }

    private void OnTriggerStay(Collider other) {

        if (ignoredTrigger == true) {
            return;
        }

        if (other.gameObject.CompareTag("Ground")) {
            //Debug.Log("Collided Ground");
            Color color = Color.white;
            ColorUtility.TryParseHtmlString(changeColor, out color);
            other.GetComponent<GroundCubeController>().changeColor(color);
        }
    }

    public void setFlick(Vector2 v)
    {
        if (ignoredTrigger == true) {
            return;
        }
        if (flickFlag == true) {
            return;
        }
        flickFlag = true;

        if (Mathf.Abs(v.x) >= Mathf.Abs(v.y)) {
            v.y = 0f;
            v.Normalize();
        } else {
            v.x = 0.0f;
            v.Normalize();
        }
        flickVector = v;
    }
}
