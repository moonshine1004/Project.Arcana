using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //조작기기를 이용하여 플레이어를 컨트롤하는 것에 대한 클래스입니다.
    
    //컴포넌트
    private Rigidbody2D _rb;
    private Animator _animator;
    //변수
    [SerializeField] private float _moveSpeed; //이동 속도
    //위치값
    private Vector3 screenPos; //마우스 클릭된 스크린 위치 값
    private Vector3 worldPos; //스크린 위치값을 월드 위치로 변환
    private Vector3 targetPos =Vector3.zero; //목표 위치
    //이동 상태
    bool isMoving = false;
    //스킬 인덱스
    public int UsingSkillSlot=0;
    [SerializeField] private UsingCardList _usingCardList;
    [SerializeField] private CardUIRenderer _cardUIRenderer;
    [SerializeField] private ProjectileLauncher _projectileLauncher;


    private void Start()
    {
        //컴포넌트 겟
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        worldPos = transform.position;
    }
    private void FixedUpdate()
    {
        Move();
    }
    //클릭 기반 이동 메서드
    private void Move()
    {
        if (isMoving)
        {
            //현재 위치와 목표 위치를 노말라이제이션하여 방향 계산
            Vector2 currentPos = _rb.position;
            Vector2 direction = ((Vector2)targetPos - currentPos).normalized;
            //목표 위치까지의 거리 계산(거리에 따라 이동 여부 변환)
            float distance = Vector2.Distance(currentPos, targetPos);
            if (distance < 0.5f) //도착하면
            {
                _rb.linearVelocity = Vector2.zero; //속도 0
                isMoving = false; //이동 해제
                _animator.SetBool(PlayerAnimatorCore.isMoving, false); //애니메이터 파라메터 변환
            }
            else //도착하지 않았으면
            {
                Vector2 newPos = currentPos + direction * _moveSpeed * Time.fixedDeltaTime;
                _rb.MovePosition(newPos);
                _animator.SetBool(PlayerAnimatorCore.isMoving, true); //애니메이터 파라메터 변환
            }
        }
        OnSetDirection();
    }
    public void OnClickMove(InputAction.CallbackContext callback)
    {
        //이벤트를 받아오면 실행되는 함수
        //이벤트는 인풋 액션 시스템의 플레이어 컨트롤러의 우클릭에 설정
        screenPos = Mouse.current.position.ReadValue(); //마우스가 클릭 된 화면(로컬) 위치 정보를 저장
        worldPos = Camera.main.ScreenToWorldPoint(screenPos); //화면 위치 정보를 월드 위치로 변환
        worldPos.z = 0;
        targetPos = worldPos; //타겟 위치를 월드 위치로 변환
        isMoving = true; //이동 해제
    }
    //방향 전환 메서드
    private void OnSetDirection()
    {
        float xDiff = targetPos.x - _rb.position.x;

        // 일정 거리 이상일 때만 방향 전환
        if (Mathf.Abs(xDiff) > 0.05f)
        {
            Vector3 scale = transform.localScale;
            if (xDiff < 0 && scale.x > 0)
            {
                scale.x *= -1;
            }
            else if (xDiff > 0 && scale.x < 0)
            {
                scale.x *= -1;
            }
            transform.localScale = scale;
        }
    }
    //스킬 발동 메서드들-->하나로 관리할 수 있도록 수정 요망
    //공격 트리거를 온, 각 공격에 해당하는 키를 온
    public void OnSkillInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            _animator.SetBool(PlayerAnimatorCore.OnSkillInput, true);
            var name = callback.control.name; //인풋 액션의 컨트롤 이름을 가져옴
            _projectileLauncher.Shoot();
            if (System.Enum.TryParse(name, out Enum.SkillKey keyEnum))//이름을 enum값에 따라 숫자로 변환
            {
                UsingSkillSlot = (int)keyEnum;
                _animator.SetInteger(PlayerAnimatorCore.SkillIndex, _usingCardList.hand[UsingSkillSlot].cardID); //애니메이터 인티저를 카드덱의 카드 아이디로 초기화
                _usingCardList.UseCard(UsingSkillSlot); //현재 스킬 인덱스에 해당하는 카드 사용
                _cardUIRenderer.cardAnimation((int)keyEnum); //입력 받은 키에 해당하는 칸의 카드 사용 애니메이션 재생

            }
        }
    }
    

}
