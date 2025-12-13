using UnityEngine;

public class ItemEnum : MonoBehaviour
{
    /// <summary>
    /// 아이템의 타입(물약, 주문서 등)
    /// </summary>
    public enum ItemType
    {
        potion = 0
    }
    /// <summary>
    /// 아이템의 효과(힐, 데미지, 이속 증가 등)
    /// </summary>
    public enum EffectType
    {
        healing = 0
    }
}
