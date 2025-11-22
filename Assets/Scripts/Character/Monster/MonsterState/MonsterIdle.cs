using Unity.VisualScripting;
using UnityEngine;

public class MonsterIdle : MonsterStateMachine, MonsterBaseState.IState
{

    //몬스터의 idle 상태를 정의한 클래스입니다.

    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Idle;

    public void Enter()
    {
        Debug.Log("idle 진입완료");
        monsterStateMachine = GetComponent<MonsterStateMachine>();
    }

    public void Exit()
    {
        Debug.Log("상태를 chase로 변경");
        monsterStateMachine.ChangeState(MonsterBaseState.MonsterState.Chase); //상태를 chase로 변경
    
    }

    public void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Damageable>() != null) //부딪힌 오브젝트가 데미지를 갖고 있을 때만 상태 변화
        {
            
            Debug.Log("충돌 발생");
            monsterStateMachine.monsterContext.Target = collision.gameObject.GetComponent<Damageable>().owner; //이 오브젝트를 타겟으로 설정
            Exit();
        }
    }
}
