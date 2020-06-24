using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayTouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public GameObject mTapEffect;
    public Camera camera;
    int counter = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        //マウスクリック時
        NewTapEffect(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }


    void Update() {
        if (counter % 100 == 0) { 
            //タップ時
            CheckTap();
        }
      counter++;
    }

    void CheckTap()
    {

        NewTapEffect(new Vector2(0, 0));
      //foreach (Touch t in Input.touches)
      //  {
       //     if (t.phase == TouchPhase.Began)
        //    {
         //       NewTapEffect(t.position);
          //  }
       // }
    }

    //タップエフェクトを出す
    void NewTapEffect(Vector2 pos)
    {
        //Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Vector2 worldPos = camera.ScreenToWorldPoint(pos);
        Object.Instantiate(mTapEffect, worldPos, Quaternion.identity, transform);
    }

}
