using System;
using UnityEngine;
/// <summary>
/// 아이템 스크립터블 오브젝트 클래스 정의
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public int id;  //아이템 id
    public ItemEnum.ItemType itemType;
    public ItemEnum.EffectType effectType;
    public string itemName;
    public int coolDown;
    public int valueMain;
    public int valueSub;
}
