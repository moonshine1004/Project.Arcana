using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
namespace study
{
    

public enum MonsterType
{
    Warrior, Archer, Sorcerer, Summoner
}

#region 인터페이스 선언
public interface IAttackable
{
    void Attack();
}
public interface IMovable
{
    void Move();
}
public interface IProjectile
{
    void Launch();
}
public interface ISummonable
{
    void Summon();
}
#endregion

#region 인터페이스 구현체
public class MonsterBasicMove : IMovable
{
    void IMovable.Move()
    {
        //MonsterBasicMove
    }
}
public class Attack01 : IAttackable
{
    public void Attack()
    {
        //attack01
    }
}
public class ProjectileLaunch01 : IProjectile
{
    public void Launch()
    {
        //01
    }
}
public class ProjectileLaunch02 : IProjectile
{
    public void Launch()
    {
        //02
    }
}
public class Summon01 : ISummonable
{
    public void Summon()
    {
        //
    }
}
#endregion



public class Monster
{
    protected IMovable _moveMethod;
    protected IAttackable _attack;
    protected IProjectile _projectileLaunch;
    protected ISummonable _summon;

    // public Monster(
    //     IMovable movable = null,
    //     IAttackable attackable = null,
    //     IProjectile projectile = null,
    //     ISummonable summon = null)
    // {
    //     _moveMethod = movable;
    //     _attack = attackable;
    //     _projectileLaunch = projectile;
    //     this._summon = summon;
    // }

    public void OnkeyInput()
    {
        _attack?.Attack();
        _moveMethod?.Move();
        _projectileLaunch?.Launch();
        _summon?.Summon();
    }
}
#region 서브클래스
public class GoblinWarrior : Monster
{
    public GoblinWarrior(IMovable moveMethod, Attack01 attack01)
    {
        _moveMethod = moveMethod;
        _attack = attack01;
    }
}
public class GoblinArcher : Monster
{
    public GoblinArcher(IMovable moveMethod, IProjectile projectile)
    {
        _moveMethod = moveMethod;
        _projectileLaunch = projectile;
    }
}
public class GoblinSorcerer : Monster
{
    public GoblinSorcerer(IProjectile projectile)
    {
        _projectileLaunch = projectile;
    }
}
public class GoblinSummoner : Monster
{
    public GoblinSummoner(IMovable moveMethod, ISummonable summon)
    {
        _moveMethod = moveMethod;
        _summon = summon;
    }
}
#endregion

public class GoblinFactory
{
    private IMovable _movable;
    private IAttackable _attackable;
    private IProjectile _projectile;
    private ISummonable _summon;

    public GoblinFactory()
    {
        
    }
    
    public Monster CreatMonster(MonsterType monsterType)
    {
        switch (monsterType)
        {
            case MonsterType.Warrior:
                return new GoblinWarrior(new MonsterBasicMove(), new Attack01());
            case MonsterType.Archer:
                return new GoblinArcher(new MonsterBasicMove(), new ProjectileLaunch01());
            case MonsterType.Sorcerer:
                return new GoblinSorcerer(new ProjectileLaunch02());
            case MonsterType.Summoner:
                return new GoblinSummoner(new MonsterBasicMove(), new Summon01());
            default:
                return null;
        }
    }
}

public class GM : MonoBehaviour
{
    private GoblinFactory _goblinFactory;
    
    // private IMovable _basicMove;
    // private IAttackable _attack01;
    // private IProjectile _arrowProjectile;
    // private IProjectile _magicProjectile;
    // private ISummonable _summonGoblin;

    private Monster _archerGoblin01;

    private MonsterBasicMove monsterBasicMove = new MonsterBasicMove();

    private void Awake()
    {
        
    }
    private void Start()
    {
        _goblinFactory = new GoblinFactory();
        _archerGoblin01 = _goblinFactory.CreatMonster(MonsterType.Archer);
        _archerGoblin01.OnkeyInput();
    }
}





/// <summary>
/// 뷰가 뷰모델에 이벤트를 알리는 바인딩을 위한 클래스
/// 유니티는 자체 바인딩을 지원하고 있지 않기 때문에 옵저버 패턴으로 수동으로 바인딩
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ObservableProperty<T>
{
    private T _value;
    public event Action<T> action; // 반환값이 없는 메서드를 담는 변수 Changed

    public T Value // _value는 프로퍼티로 열어둠
    {
        get => _value;
        set
        {
            if(!Equals(_value, value)) // _value값에 변화가 있을 때 
            {
                _value = value; // _value를 변한값으로 수정 후
                action?.Invoke(_value); // 이벤트를 통해 리스너에게 값과 함께 알림
            }
        }
    }
    public ObservableProperty(T initialValue = default) // 생성자로 ObservableProperty가 생성될 때 리턴값을 지정
    {
        _value = initialValue;
    }
}
/// <summary>
/// 커맨드를 정의한 인터페이스
/// 커맨드는 '실행'과 '실행 가능 여부'를 캡슐화한 것
/// MVVM에서 커맨드 
/// </summary>
public interface ICommand
{
    bool CanExcute(); // 실행 여부
    void Excute(); // 실제 실행
}
public sealed class DelegateCommand : ICommand
{
    private readonly Func<bool> _canExcute; // 실행 조건을 정의한 메서드를 담는 변수 _canExcute 
    private readonly Action _excute; // 실행될 메서드를 담는 변수 _excute

    public DelegateCommand(Action excute, Func<bool> canExcute = null) // 생성자로 주입 실행 조건을 담은 메서드와 실행할 메서드를 주입
    {
        _excute = excute;
        _canExcute = canExcute;
    }
    
    public bool CanExcute() => _canExcute?.Invoke() ?? true;

    public void Excute() // 이 메서드가 호출되면 실행 조건을 정의한 메서드(_canExcute)가 실행되고 반환값에 따라 _excute의 메서드가 실행
    {
        if (_canExcute()) _excute(); 
    }
}

public class PlayerInstaller : MonoBehaviour // 플레이어 오브젝트를 관리하는 클래스 
{
    [SerializeField] private PlayerView _playerView; // 뷰의 구현체만 SerializeField어트리뷰트로 받아옴

    public void Awake()
    {
        var model = new PlayerModel(); // 모델은 일반 C#스크립트이므로 new키워드로 직접생성
        var viewModel = new PlayerViewModel(model); // 뷰모델을 생성하며 생성자로 모델 주입

        _playerView.InitializeView(viewModel); // 뷰에 프리젠터 주입
    }
}
/// <summary>
/// 모델은 순수 데이터와 데이터 제어에 관한 로직만 가짐
/// 엔진의 상태와 관계 없는 논리적 데이터, 값 등만 가지며, 이를 네트워크 동기화 등을 통해 세이브/로드함
/// </summary>
public class PlayerModel
{
    private string _name;
    private int _hp;
    public Vector3 position;
    public int HP{ get => _hp; }
    public bool isMoving = false;
    public float speed = 5.0f;
    public void OnMove(Vector2 direction, float deltaTime)
    {
        position = direction * (speed * deltaTime);
    }
    public void OnHpChange(int damage)
    {
        _hp -= damage;
    }
}



/// <summary>
/// 뷰는 MonoBehaviour를 상속받아 엔진과의 상호작용을 할 수 있음
/// 즉, 게임 오브젝트에 붙어 엔진이 관리하는 게임 오브젝트의 엔진 상태를 조정
/// </summary>
public class PlayerView : MonoBehaviour
{
    private PlayerModel _playerModel;
    private PlayerViewModel _playerViewModel;

    public void InitializeView(PlayerViewModel playerViewModel)
    {
        _playerViewModel = playerViewModel;
    }
    public void Awake()
    {
        _playerModel = new PlayerModel();
        _playerViewModel = new PlayerViewModel(_playerModel);
    }
    public void OnEnable()
    {
        // 수동으로 상태 구독
        _playerViewModel.UpdatePosition.action += OnPositionChanged;
        _playerViewModel.UpdateHp.action += OnHpChanged;

        OnPositionChanged(_playerViewModel.UpdatePosition.Value);
        OnHpChanged(_playerViewModel.UpdateHp.Value);
    }
    private void OnDisable()
    {
        // 상태 구독 해제
        _playerViewModel.UpdatePosition.action -= OnPositionChanged;
        _playerViewModel.UpdateHp.action -= OnHpChanged;
    }
    private void OnPositionChanged(Vector3 pos)
    {
        transform.position = pos; // 뷰의 변화
        _playerViewModel.MoveCommand.Excute();
    }
    private void OnHpChanged(int damage)
    {
        _playerViewModel.OnHit(damage);
    }
    private void OnMoveInput(InputAction.CallbackContext callback)
    {
        var dir = callback.ReadValue<Vector2>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>(); // MonoBehaviour를 상속받는 뷰가 데미지 추출
        OnHpChanged(damageable.damage);
    }
}
public class PlayerViewModel
{
    private PlayerModel _playerModel;
    
    public ObservableProperty<Vector3> UpdatePosition { get; } = new ObservableProperty<Vector3>(); //Vector3를 매게변수로 하는 메서드를 담는 변수;
    public ObservableProperty<int> UpdateHp { get; } = new ObservableProperty<int>();
    
    public ICommand MoveCommand{ get; }

    public PlayerViewModel(PlayerModel playerModel)
    {
        _playerModel = playerModel;

        // 초기값 적용
        UpdatePosition.Value = _playerModel.position;
        UpdateHp.Value = _playerModel.HP;

        // 커맨드 등록
        MoveCommand = new DelegateCommand(OnMove);
    }

    public void OnMove()
    {
        
    }
    public void OnHit(int damage)
    {
        _playerModel.OnHpChange(damage);
    }
}

}





