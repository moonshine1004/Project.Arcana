using UnityEngine;

public class Enum : MonoBehaviour
{
    //enum을 관리하는 클래스입니다.
    
    /// <summary>
    /// 카드 속성 관리 enum
    /// </summary>
    public enum Element
    {
        Normal = 0,
        Fire = 1,
        Ice = 2,
        Wind =3,
        Rock = 4,
        Elect =5,
    }
    /// <summary>
    /// 카드 범위 enum
    /// </summary>
    public enum RangeType
    {
        Projectile	= 0,
        Line = 1,
        Close = 2,
        Cone = 3,
        Circle = 4,

    }
    /// <summary>
    /// 카드 타겟 타입 enum
    /// </summary>
    public enum TargetType
    {
        NoneTarget = 0,
        SelfTarget = 1,
        EnemyTarget = 2,
        AllTarget = 3,
    }
    /// <summary>
    /// 컨트롤러 키에 번호 할당
    /// </summary>
    public enum SkillKey
    {
        q = 0, w = 1, e = 2, r = 3, t = 4
    }
}
