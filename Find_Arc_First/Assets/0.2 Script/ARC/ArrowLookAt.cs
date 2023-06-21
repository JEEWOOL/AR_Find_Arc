using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowLookAt : MonoBehaviour
{
    public GameObject target;
    public GameObject textObj;
    public TMP_Text disText;

    private Camera mCamera;
    void Start()
    {
        mCamera = Camera.main;
    }
    void Update()
    {
        if(target != null)
        {
            this.transform.LookAt(target.transform);
            float dis = (target.transform.position - mCamera.transform.position).magnitude;
            disText.text = dis.ToString() + "m";
        }
        textObj.transform.LookAt(mCamera.transform.position);
        textObj.transform.rotation = Quaternion.Euler(-textObj.transform.rotation.eulerAngles.x, textObj.transform.rotation.eulerAngles.y + 180
            , textObj.transform.rotation.eulerAngles.z);
    }
}
