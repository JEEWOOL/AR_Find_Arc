using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public static DistanceManager instance;
    public int checkDis = 100;
    void Start()
    {
        if (instance == null)
            instance = this;

        checkDis = 200;
    }

}
