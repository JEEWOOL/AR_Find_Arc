using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectCreateManager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject renderPref;

    ARRaycastManager raycastManager;
    void Start()
    {
        indicator.SetActive(false);
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();
        if(indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Instantiate(renderPref, indicator.transform.position, indicator.transform.rotation);
            }
        }
    }

    private void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        if(raycastManager.Raycast(screenSize, hitInfos, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            indicator.SetActive(true);
            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;

            indicator.transform.position += indicator.transform.up * 0.001f;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
