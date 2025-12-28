using UnityEngine;

//Card라는 이름의 파일을 만듦
[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
//스크립터블 오브젝트에 대해 정의하는 스크립트
public class CardScriptableObject : ScriptableObject
{
    //카드 스크립터블 오브젝트 클래스 입니다
    public int cardID;
    public int cost;
    public float damage;
    public Enum.Element element;
    public Enum.RangeType rangeType;
    public Enum.TargetType targetType;



}
