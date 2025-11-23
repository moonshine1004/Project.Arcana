using UnityEngine;

public class MonsterAttackState : MonsterStateMachine, MonsterBaseState.IState
{
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Attack;

    //몬스터의 공격 상태를 정의한 클래스입니다.
    #region 필드 변수
    [SerializeField]
    private GameObject _target; //타겟 오브젝트
    #endregion
    
    public void Enter()
    {
        _target = monsterContext.Target;
    }

    public void Exit()
    {
        //
    }

    public void Update()
    {
        //
    }
}
