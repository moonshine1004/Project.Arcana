using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f; //이동 속도
    private bool _isMoving; //이동 상태

    public void MonsterMoving(Transform targetPos)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,                // 시작점
            targetPos.position,                   // 목표점
            _speed * Time.deltaTime             // 한 프레임 이동 거리
        );
    }

}
