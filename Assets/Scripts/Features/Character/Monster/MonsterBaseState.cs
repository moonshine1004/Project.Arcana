using UnityEngine;

public class MonsterBaseState : MonoBehaviour
{
    //캐릭터의 
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
        MonsterState StateType
        {
            get;
        }
    }
    
    public enum MonsterState //몬스터의 상태
    {
        Idle,
        Chase,
        Attack,
        Back
    }



}
