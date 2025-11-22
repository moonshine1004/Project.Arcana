using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
        
    }
    public int damage;
    public GameObject owner; //이 오브젝트를 발사한 오브젝트

}
