using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MakeArrow : MonoBehaviour
{
    public static MakeArrow instance;
    public GameObject arrowPref;
    public GameObject arrowParrent;
    public List<GameObject> arrows;
    private Camera m_cam;
    void Start()
    {
        if(instance == null)
            instance = this;
        m_cam = Camera.main;
    }
    void Update()
    {
        arrowParrent.transform.position = m_cam.transform.position;
        //Debug.Log(arrowParrent.transform.position);
    }
    public void MakeArrows()
    {
        int i = 0;
        List<GameObject> boxes = BoxManager.instance.boxes;
        for(i = 0; i < boxes.Count; i++)
        {
            if (boxes[i] != null)
            {
                if(i < arrows.Count)
                {
                    arrows[i].GetComponent<ArrowLookAt>().target = boxes[i];
                }
                else
                {
                    GameObject arrow = Instantiate(arrowPref, arrowParrent.transform);
                    arrow.GetComponent<ArrowLookAt>().target = boxes[i];
                    Debug.Log("Add Markers " + boxes[i].transform.position);
                    arrows.Add(arrow);
                }
            }
            else
            {
                boxes.RemoveAt(i);
                i--;
            }
        }
        for (; i < arrows.Count; i++)
        {
            if (arrows[i] != null)
            {
                Destroy(arrows[i]);
                arrows.RemoveAt(i);
            }
        }
        //foreach (GameObject box in boxes)
        //{
        //    if(box != null)
        //    {
        //        GameObject arrow = Instantiate(arrowPref, arrowParrent.transform);
        //        arrow.GetComponent<ArrowLookAt>().target = box;
        //        Debug.Log("Add Markers " + box.transform.position);
        //        arrows.Add(arrow);
        //    }
        //}
    }


}
