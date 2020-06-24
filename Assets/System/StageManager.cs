using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TouchScript.Gestures;
using System;

public class StageManager : MonoBehaviour
{
    public Camera camera;
    public GameObject tapEffect;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        GetComponent<TapGesture>().Tapped += OnTapped;
        GetComponent<FlickGesture>().Flicked += OnFlicked;
    }

    private void OnDisable() {
        GetComponent<TapGesture>().Tapped -= OnTapped;
        GetComponent<FlickGesture>().Flicked -= OnFlicked;
    }

    private void OnTapped(object sender, EventArgs e) {
        var gesture = sender as TapGesture;
        Vector3 pos = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, 1);
        Vector2 worldPos = camera.ScreenToWorldPoint(pos);
        //Object.Instantiate(tapEffect, worldPos, Quaternion.identity, transform);

//        Vector3 pos = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, 1);
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Instantiate(tapEffect, worldPos, Quaternion.identity);
    }

    private void OnFlicked(object sender, EventArgs e) {
        var gesture = sender as FlickGesture;
        ball.GetComponent<BallController>().setFlick(gesture.ScreenFlickVector);
    }

}
