using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DirectionChecker : MonoBehaviour
{
    public TMP_Text[] tArr;
    private bool isOn = true;
    private Quaternion exRot;
    public Vector2 exGpsPos = Vector2.zero;
    public Vector2 gpsPos = Vector2.zero;
    public Vector3 exCameraPos = Vector3.zero;
    public Vector3 cameraPos = Vector3.zero;

    public GameObject origin;
    private Camera mCamera;

    public static DirectionChecker instance;
    void Start()
    {
        if(instance == null) instance = this;
        mCamera = Camera.main;
    }
    public void DebugOnOff()
    {
        isOn = !isOn;
        for(int i =0;i< tArr.Length; i++)
        {
            tArr[i].gameObject.SetActive(isOn);
        }
    }
    public void SetGpsPos(Vector2 pos)
    {        
        //exGpsPos = gpsPos;
        //gpsPos = pos;
        ////exCameraPos = cameraPos;
        //cameraPos = new Vector3(mCamera.transform.position.x,0, mCamera.transform.position.z);
        //cameraPos += new Vector3(CameraPosManager.instance.cameraParrent.transform.position.x, 
        //    0, CameraPosManager.instance.cameraParrent.transform.position.z);

        GPSEncoder.SetLocalOrigin(pos);
        FireBaseCheck.instance.UpdateBoxAndArrow();

        //tArr[0].text = $"exGpsPos {exGpsPos}, gpsPos {gpsPos}, exCameraPos {exCameraPos}, cameraPos {cameraPos}";

        //if (exGpsPos.x == 0 || exGpsPos.y == 0)
        //    return;

        //Vector3 curGpsUCS = GPSEncoder.GPSToUCS(gpsPos);
        //Vector3 exGpsUCS = GPSEncoder.GPSToUCS(exGpsPos);

        //Vector3 gpsDir = (curGpsUCS - exGpsUCS).normalized;
        //Vector3 cameraDir = (cameraPos).normalized;
        //float dot = Vector3.Dot(cameraDir, gpsDir);
        //float angle = Mathf.Acos(dot);
        //float dig = angle * Mathf.Rad2Deg;
        //Vector3 outter = Vector3.Cross(cameraDir, gpsDir);
        //float sign = Mathf.Sign(outter.z);

        //dig = -sign * dig;

        //dig = dig - exRot.eulerAngles.y;
        //tArr[1].text = $"gpsDir {gpsDir}, cameraDir {cameraDir}, angle {angle}, sign {sign}, dig {dig}";

        //BoxManager.instance.UpdateInrangeBoxes();
        
        //tArr[2].text = $"b origin.transform.rotation {origin.transform.rotation}";
        //CameraPosManager.instance.UpdateCamera();
        //origin.transform.rotation = Quaternion.Euler(origin.transform.rotation.eulerAngles.x,
        //    origin.transform.rotation.eulerAngles.y + dig, origin.transform.rotation.eulerAngles.z);
        //exRot = origin.transform.rotation;
        //tArr[3].text = $"f origin.transform.rotation {origin.transform.rotation}";
    }
}
