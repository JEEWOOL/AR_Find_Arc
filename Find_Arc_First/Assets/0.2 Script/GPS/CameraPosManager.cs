using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosManager : MonoBehaviour
{
    public static CameraPosManager instance;
    public GameObject cameraParrent;
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdateCamera()
    {
        Vector3 tPos = cameraParrent.transform.position - Camera.main.transform.position;
        cameraParrent.transform.position = tPos;
    }
}
