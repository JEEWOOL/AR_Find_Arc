using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public string databaseUrl = "https://arrrr-c617e-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public static DBManager instance;

    bool isSearch = false;

    Vector2 curPos;

    void Start()
    {
        if(instance == null)
            instance = this;
        //FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(databaseUrl);
        SaveData();
        StartCoroutine(LoadData(Vector2.one, transform));
    }

    void SaveData()
    {
        ImageGPSData data1 = new ImageGPSData("Cat", 37.48985f, 126.9601f, false);
        ImageGPSData data2 = new ImageGPSData("SCar", 37.48085f, 126.9591f, false);

        string jsonCat = JsonUtility.ToJson(data1);
        string jsonSCar = JsonUtility.ToJson(data2);

        DatabaseReference refData = FirebaseDatabase.GetInstance(databaseUrl).RootReference;
        refData.Child("Markers").Child("Data1").SetRawJsonValueAsync(jsonCat);
        refData.Child("Markers").Child("Data2").SetRawJsonValueAsync(jsonSCar);
    }

    public IEnumerator LoadData(Vector2 pos, Transform trackedImage)
    {
        curPos = pos;
        DatabaseReference refData = FirebaseDatabase.GetInstance(databaseUrl).GetReference("Markers");
        isSearch = true;
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
                    ImageGPSData cData = JsonUtility.FromJson<ImageGPSData>(sData);
                    cData.ShowData();
                }
            }
            isSearch = false;
        });
        while(isSearch)
            yield return null;
    }

}

public class ImageGPSData {
    public string name;
    public float latitude;
    public float longitude;
    public bool isCaptured = false;

    public ImageGPSData(string objName, float lat, float lon, bool captured)
    {
        name = objName;
        latitude = lat;
        longitude = lon;
        isCaptured = captured;
    }
    public void ShowData()
    {
        Debug.Log(name + " " + latitude.ToString() + " " + longitude.ToString() + " " + isCaptured.ToString());
    }
}
