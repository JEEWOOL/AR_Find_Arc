using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject treasureBox;
    public Transform parents;
    public TMP_Text GetCheckText;
    public CardAsset[] cardAsset;
    public List<CardAsset> cardList = new List<CardAsset>();

    public GameObject go_Item;
    Scene scene;
    static private GameManager _instance;
    static public GameManager Instance { get { return _instance; } }
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;
        scene = SceneManager.GetActiveScene();
    }
}