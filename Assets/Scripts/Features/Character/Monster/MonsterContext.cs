using UnityEngine;



public class MonsterContext
{
    public GameObject Owner { get; set; } //이 오브젝트
    public Collider2D OwnerCollider { get; set; } //이 오브젝트의 콜라이더
    public GameObject Target { get; set; } //타겟 오브젝트

}