using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card Asset", menuName = "Scriptable Object/Card Asset", order = int.MaxValue)]
public class CardAsset : ScriptableObject
{
    public int cardNum;
    public int damage;
    public int health;
    public int cardCount;
    public Sprite cardImage;
    public string cardName;
}
