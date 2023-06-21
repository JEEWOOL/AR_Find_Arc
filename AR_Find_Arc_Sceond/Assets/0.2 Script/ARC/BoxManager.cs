using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager instance;
    public GameObject boxPref;
    public GameObject boxParrent;
    public List<GameObject> boxes = new List<GameObject>();
    void Start()
    {
        if(instance == null)
            instance = this;
    }
    public void Init()
    {
        //foreach(GameObject obj in boxes)
        //{
        //    GameObject temp = obj;
        //    boxes.Remove(obj);
        //    Destroy(temp);
        //}
        UpdateInrangeBoxes();
    }
    void MakeInrangeBoxes()
    {
        List<BoxData> boxDatas = FireBaseCheck.instance.inRangeBoxes;
        foreach(BoxData boxData in boxDatas)
        {
            Vector3 pos =GPSEncoder.GPSToUCS((float)boxData.lat, (float)boxData.lon);
            boxes.Add(Instantiate(boxPref, pos, Quaternion.identity, boxParrent.transform));
        }
    }
    public void UpdateInrangeBoxes()
    {
        List<BoxData> boxDatas = FireBaseCheck.instance.inRangeBoxes;
        int i = 0;
        for (i =0; i< boxDatas.Count;i++)
        {
            Vector3 pos = GPSEncoder.GPSToUCS((float)boxDatas[i].lat, (float)boxDatas[i].lon);
            if (i < boxes.Count)
            {
                if(boxes[i] == null)
                {
                    boxes.Add(Instantiate(boxPref, pos, Quaternion.identity, boxParrent.transform));
                    continue;
                }
                boxes[i].transform.position = pos;
            }
            else
            {
                boxes.Add(Instantiate(boxPref, pos, Quaternion.identity, boxParrent.transform));
            }
        }
        for(i = boxDatas.Count; i<boxes.Count;i++)
        {
            Destroy(boxes[i]);
            boxes[i] = null;
        }
    }
}
