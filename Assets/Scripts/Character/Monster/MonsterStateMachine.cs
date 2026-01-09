using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MonsterStateMachine : MonoBehaviour
{

    protected MonsterBaseState.MonsterState currentState; //몬스터 상태를 복사
    public MonsterContext monsterContext{ get; protected set; } //몬스터의 기본적인 정보가 담긴 데이터 컨테이너

    public MonsterStateMachine monsterStateMachine{ get; protected set; }
    private Dictionary<MonsterBaseState.MonsterState, MonsterBaseState.IMonsterState> _state; //몬스터의 스테이트를 키로하고 몬스터의 스테이트 인터페이스(의 enum값)를 값으로 하는 딕셔너리


    private void Start()
    {
        
    }
    private void Awake()
    {
        var found = GetComponents<MonoBehaviour>().OfType<MonsterBaseState.IMonsterState>(); //오브젝트의 MonsterStateMachine 컴포넌트 중 MonsterBaseState.IState 인터페이스를 갖고 있는 컴포넌트만 IEnumerable형 변수 found에 넣음
        _state = new Dictionary<MonsterBaseState.MonsterState, MonsterBaseState.IMonsterState>(); //새 딕서너리를 만듦

        monsterContext = new MonsterContext //몬스터의 데이터 컨테이너 복사
        {
            Owner = gameObject,
            OwnerCollider = GetComponent<Collider2D>(),
            Target = null
        };
        foreach (var st in found) //found에서 st를 하나씩 꺼내어
        {
            _state[st.StateType] = st; //st.StateType을 키로 하는 값에 st을 넣음
        } 
        currentState = MonsterBaseState.MonsterState.Idle; //시작 상태는 idle
        _state[currentState].Enter(); //시작 상태로 진입
        //Debug.Log("시작상태로 진입");
    }

    //몬스터의 state가 변할 때 호출되는 메서드
    public void ChangeState(MonsterBaseState.MonsterState nextState)
    {
        _state[nextState].Enter(); //딕셔너리 키에 따라 다음 스테이트 인터페이스의 Enter를 호출
    }



}




