using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InitializeView(IPlayerPresenter playerPresenter);
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    public void InitializeView(IPlayerPresenter playerPresenter){ _playerPresenter = playerPresenter; }
    
    #region Components
    private Rigidbody2D _rb;
    private Animator _animator;
    #endregion 

    #region Feild
    [SerializeField] private float _moveSpeed; 
    private Vector3 _screenPos; //마우스 클릭된 스크린 위치 값
    private Vector3 _worldPos; //스크린 위치값을 월드 위치로 변환
    private Vector3 _targetPos =Vector3.zero; //목표 위치
    private bool _isMoving = false;
    #endregion
    
    #region Presenters
    private ICardPresenter _cardPresenter;
    #endregion

    [SerializeField] private CardUIRenderer _cardUIRenderer;
    [SerializeField] private ProjectileLauncher _projectileLauncher;


    private void Start()
    {
        //컴포넌트 겟
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _worldPos = transform.position;
    }
    private void FixedUpdate()
    {
        Move();
    }
    //클릭 기반 이동 메서드
    private void Move()
    {
        if (_isMoving)
        {
            //현재 위치와 목표 위치를 노말라이제이션하여 방향 계산
            Vector2 currentPos = _rb.position;
            Vector2 direction = ((Vector2)_targetPos - currentPos).normalized;
            //목표 위치까지의 거리 계산(거리에 따라 이동 여부 변환)
            float distance = Vector2.Distance(currentPos, _targetPos);
            if (distance < 0.5f) //도착하면
            {
                _rb.linearVelocity = Vector2.zero; //속도 0
                _isMoving = false; //이동 해제
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
        _screenPos = Mouse.current.position.ReadValue(); //마우스가 클릭 된 화면(로컬) 위치 정보를 저장
        _worldPos = Camera.main.ScreenToWorldPoint(_screenPos); //화면 위치 정보를 월드 위치로 변환
        _worldPos.z = 0;
        _targetPos = _worldPos; //타겟 위치를 월드 위치로 변환
        _isMoving = true; //이동 해제
    }
    //방향 전환 메서드
    private void OnSetDirection()
    {
        float xDiff = _targetPos.x - _rb.position.x;

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
        if (callback.performed)
        {
            _animator.SetBool(PlayerAnimatorCore.OnSkillInput, true);
            var name = callback.control.name; //인풋 액션의 컨트롤 이름을 가져옴
            if (System.Enum.TryParse(name, out Enum.SkillKey keyEnum))//이름을 enum값에 따라 숫자로 변환
            {
                int index = (int)keyEnum;
                _projectileLauncher.Shoot(index);
                int thisCard = _playerPresenter.OnUseCard(index);
                _animator.SetInteger(PlayerAnimatorCore.SkillIndex, thisCard); //애니메이터 인티저를 카드덱의 카드 아이디로 초기화
                _cardUIRenderer.cardAnimation((int)keyEnum); //입력 받은 키에 해당하는 칸의 카드 사용 애니메이션 재생

            }
        }
    }

}
