using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CardManager : MonoBehaviour
{
    Camera mCam;

    private void Start()
    {
        mCam = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray;
            RaycastHit hitobj;
            ray = mCam.ScreenPointToRay(touch.position);
            //Ray를 통한 오브젝트 인식
            if (Physics.Raycast(ray, out hitobj))
            {
                if (hitobj.collider.CompareTag("Box"))
                {
                    BoxManager.instance.boxes.Remove(hitobj.collider.gameObject);
                    //FireBaseCheck.instance.inRangeBoxes.Remove(hitobj.collider.gameObject);
                    Destroy(hitobj.collider.gameObject);

                    AddCard();

                    //GameManager.Instance.GetCheckText.text = "Tresure Opened";                    
                }
            }
        }
    }

    public void AddCard()
    {
        GameManager.Instance.cardList.Add(GameManager.Instance.cardAsset[0]);
        Debug.Log("카드정보 추가");
    }

    //public void GetTreasure(Vector2 touchPos)
    //{
    //    //Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    //    List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

    //    if (arManager.Raycast(touchPos, hitInfos, TrackableType.AllTypes))
    //    {
    //        Debug.Log(hitInfos);
    //        Destroy(GameManager.Instance.treasureBox);
    //        GameManager.Instance.GetCheckText.text = "Get! ITEM";

    //        Card cd = new Card();
    //        cd.cardNum = GameManager.Instance.cardAsset[0].cardNum;
    //        cd.damage = GameManager.Instance.cardAsset[0].damage;
    //        cd.health = GameManager.Instance.cardAsset[0].health;
    //        cd.cardCount = GameManager.Instance.cardAsset[0].cardCount;
    //        cd.cardImage.sprite = GameManager.Instance.cardAsset[0].cardImage;
    //        cd.cardName = GameManager.Instance.cardAsset[0].cardName;
    //        GameManager.Instance.cardList.Add(cd);
    //    }
    //}
}