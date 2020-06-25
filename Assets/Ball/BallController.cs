﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        flickFlag = false;
        flickVector = new Vector2(0, 0);
        //        int pointX = 3;
        //       int pointY = 3;

        //        this.gameObject.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(3, 5), Random.Range(-5, 5));
        //       LeanTween.move(this.gameObject, new Vector3(pointX + 0.5f, 0.5f, pointY + 0.5f), 1.0f);

        //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;


        oldPosition = this.gameObject.transform.position;
        //this.gameObject.transform.position = new Vector3(pointX + 0.5f, 0.5f, pointY + 0.5f);
        //LeanTween.delayedCall(this.gameObject, 1.0f, ()=>{
        //    //            LeanTween.move(gameObject, gameObject.transform.position + new Vector3(9f, 0f, 1f), 1f).setEase(LeanTweenType.easeInQuad);
        //});
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーション中は物理演算を無効にする。
        //this.gameObject.GetComponent<Rigidbody>().isKinematic = LeanTween.isTweening(this.gameObject);
        //Debug.Log("Update:flickFlag:" +flickFlag.ToString());

        //フリック
        if (flickFlag == true) {
            Debug.Log("Update.flickFlag");
            flickFlag = false;
            if(flickVector.x != 0f) {
                this.gameObject.transform.position += new Vector3(flickVector.x, 0f, 0);
                Debug.Log("FlickX:" + flickVector.x.ToString());
            }
            if (flickVector.y != 0f) {
                this.gameObject.transform.position += new Vector3(0, 0, flickVector.y);
                Debug.Log("FlickY:" + flickVector.y.ToString());
            }
        }

        if (oldPosition != this.gameObject.transform.position) {
            if (counter % 4 == 0) {
                if (ballAfterImage != null) {
                    Instantiate(ballAfterImage, this.gameObject.transform.position, Quaternion.identity);
                }
            }
        }
        oldPosition = this.gameObject.transform.position;


        if(counter % 60 == 0) {
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
            Debug.Log("Collided Wall!");
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided Ground");
            other.GetComponent<GroundCubeController>().changeColor(Color.green);
        }
    }

    public void setFlick(Vector2 v)
    {
        Debug.Log("SetFlick:" + ignoredTrigger.ToString() + ":" + flickFlag.ToString());
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
        Debug.Log("SetFlick:" + v);
        flickVector = v;
    }
}
