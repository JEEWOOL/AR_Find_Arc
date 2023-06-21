using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public void GoCardScene()
    {
        SceneManager.LoadScene("CardItem");
    }
    public void GoARScene()
    {
        SceneManager.LoadScene("AR");
    }
}
