using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class GPSManager : MonoBehaviour
{
    public TMP_Text latutude_text;
    public TMP_Text longitude_text;

    public float maxWaitTime = 5.0f;
    float waitTime = 0;
    bool recieveGPS = false;

    public float latitude = 0;
    public float longitude = 0;
    private int x;
    void Start()
    {
        StartCoroutine(GPS_On());
        x = 0;
    }

    public IEnumerator GPS_On()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }
        if (!Input.location.isEnabledByUser)
        {
            latutude_text.text = "GPS_OFF";
            longitude_text.text = "GPS_OFF";
        }

        Input.location.Start();
        waitTime = 0;
        while (Input.location.status == LocationServiceStatus.Initializing && waitTime < maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        }
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            latutude_text.text = "GPS_FAILED";
            longitude_text.text = "GPS_FAILED";
        }
        if(waitTime >= maxWaitTime)
        {
            latutude_text.text = "RESPONSE_TIME_OVER";
            longitude_text.text = "RESPONSE_TIME_OVER";
        }

        LocationInfo li;

        latutude_text.text = "latutude: " + latitude.ToString();
        longitude_text.text = "longitude: " + longitude.ToString();

        recieveGPS = true;

        while(recieveGPS)
        {
            yield return new WaitForSeconds(1f);
            li = Input.location.lastData;
            DirectionChecker.instance.tArr[4].text = $"{x++}, li.latitude {li.latitude}, li.longitude {li.longitude}";

            if (latitude == li.latitude && longitude == li.longitude)
                continue;
            latitude = li.latitude;
            longitude = li.longitude;

            DirectionChecker.instance.SetGpsPos(new Vector2(latitude, longitude));

            latutude_text.text = "latutude: " + latitude.ToString();
            longitude_text.text = "longitude: " + longitude.ToString();
        }
    }
}
