using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBaseCheck : MonoBehaviour
{
    public string databaseUrl = "https://arrrr-c617e-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public static FireBaseCheck instance;
    public List<BoxData> boxes = new List<BoxData>();
    public List<BoxData> inRangeBoxes = new List<BoxData>();
    bool isSearch = false;

    void Start()
    {
        if (instance == null)
            instance = this;
        StartCoroutine(LoadData(transform));
    }
    public IEnumerator CheckData()
    {
        while (true)
        {
            if (DirectionChecker.instance.gpsPos.x <= 30 && DirectionChecker.instance.gpsPos.y <= 120)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("CurPos : " + DirectionChecker.instance.gpsPos.x + " " + DirectionChecker.instance.gpsPos.y);
                break;
            }
        }
        UpdateBoxAndArrow();
    }

    public void UpdateBoxAndArrow()
    {
        inRangeBoxes.Clear();
        foreach (BoxData box in boxes)
        {
            CheckDistance(box);
        }
        AddTestData(1000,37.57053, 126.9851);
        AddTestData(1001,37.57032, 126.9851);
        AddTestData(1002,37.57014, 126.9851);
        AddTestData(1003,37.57093, 126.9851);
        BoxManager.instance.Init();
        MakeArrow.instance.MakeArrows();
    }

    public IEnumerator LoadData(Transform trackedImage)
    {
        DatabaseReference refData = FirebaseDatabase.GetInstance(databaseUrl).GetReference("Box");
        isSearch = true;
        boxes = new List<BoxData>();
        refData.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("DB Failed");
            }
            else if (t.IsCanceled)
            {
                Debug.Log("DB Canceled");
            }
            else if (t.IsCompleted)
            {
                DataSnapshot snapShot = t.Result;
                foreach (DataSnapshot data in snapShot.Children)
                {
                    string sData = data.GetRawJsonValue();
                    BoxData cData = JsonUtility.FromJson<BoxData>(sData);
                    Debug.Log(cData.lat + " " + cData.lon);
                    boxes.Add(cData);
                }
            }
            isSearch = false;
        });
        while (isSearch)
            yield return null;
        //StartCoroutine(CheckData());
    }
    private void AddTestData(int num, double lat, double lon)
    {
        BoxData data = new BoxData(false, lat, lon);
        data.num = num;
        inRangeBoxes.Add(data);
    }
    private void CheckDistance(BoxData cData)
    {
        //float dis = Vector3.Magnitude(curtpos - GPSEncoder.GPSToUCS(cData.GetVector()));
        float dis = GPSEncoder.GPSToUCS(cData.GetVector()).magnitude;
        //Debug.Log(dis);
        if(dis < DistanceManager.instance.checkDis)
        {
            Debug.Log(dis + " : " + cData.lat + " , " + cData.lon);
            inRangeBoxes.Add(cData);
        }
    }
}

public class BoxData
{
    public int num;
    public bool isFounded = false;
    public double lat;
    public double lon;

    public BoxData(bool isf, double lat, double lon)
    {
        this.isFounded = isf;
        this.lat = lat;
        this.lon = lon;
    }
    public Vector2 GetVector()
    {
        return new Vector2((float)lat, (float)lon);
    }
}
