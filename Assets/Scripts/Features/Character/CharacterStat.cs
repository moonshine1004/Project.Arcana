using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterStat : MonoBehaviour
{
    public UnityEvent<int> GetHit;
    private Animator _animator;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    private bool _isAlive = true;
    private MonsterPooling _monsterPooling;

    public Slider healthSlider;

    private int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void Start()
    {
        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth; //healthSlider 초기화
    }
    private void Awake()
    {
        // 코드로 이벤트 연결
        GetHit.AddListener(TakeDamage);
    }
    private void Update()
    {
        healthSlider.value = _health; //healthSlider변화
        if (_health <= 0)
        {
            _isAlive = false;
        }
        if (!_isAlive)
        {
            //_animator.SetBool(MonsterAnimationCore.isAlive, false);
            _monsterPooling.OnMonsterRelease(gameObject);
        }
    }
    private void OnDestroy()
    {
        // 구독 해제
        GetHit.RemoveListener(TakeDamage);
    }

    //데미지를 입을 때 발생하는 상황 모두 넣어둠
    public void TakeDamage(int damage)
    {
        Health -= damage; //Health 프로퍼티로 _health-damage
        healthSlider.value = _health; //healthSlider변화
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌하는 오브젝트의 Damageabla 컴포넌트를 가져옴
        Damageable damage = collision.gameObject.GetComponent<Damageable>();
        if (damage != null)
        {
            //GetHit 이벤트를 데미지를 매게변수로 하여 이벤트
            GetHit.Invoke(damage.damage);
        }

    }
    public void SetPool(MonsterPooling pool)
    {
        _monsterPooling = pool;
    }
    public void StatReset()
    {
        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth;
        _isAlive = true;
    }
}
