using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCard : MonoBehaviour
{
    public Transform parents;
    public GameObject go_item;

    private void Start()
    {
        if(GameManager.Instance.cardList != null)
        {
            for (int i = 0; i < GameManager.Instance.cardList.Count; i++)
            {
                GameObject obj = Instantiate(go_item, parents);
                Card cd = obj.GetComponent<Card>();
                cd.damage.text = GameManager.Instance.cardList[i].damage.ToString();
                cd.health.text = GameManager.Instance.cardList[i].health.ToString();
                cd.cardCount.text = GameManager.Instance.cardList[i].cardCount.ToString();
                cd.cardImage.sprite = GameManager.Instance.cardList[i].cardImage;
                cd.cardName.text = GameManager.Instance.cardList[i].cardName.ToString();
            }
        }
    }
}
