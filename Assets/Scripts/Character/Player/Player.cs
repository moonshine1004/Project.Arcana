using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// player 동작의 전반을 다루는 클래스
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] private UsingCardList _UsingCardList;
    [SerializeField] private CardPooling _cardPooling; //카드 풀 위임
    [SerializeField] private List<CardModel> _cardData; //카드 데이터(스크립터블 오브젝트) 리스트
    private CardView _currentCard; //카드 프리팹
    [SerializeField] private PlayerController _playerController; //현재 사용 중인 카드 인덱스를 위임 받아옴
    [SerializeField] private CardUIRenderer _cardUIRenderer;




    private void Start()
    {




    }
    private void Update()
    {
        _cardData = new List<CardModel>(_UsingCardList.hand);
        _UsingCardList.AddCardToHand(_cardData);
    }
    //스킬 액션 이벤트가 인보크 되면 카드 풀에 있는 카드를 꺼내 옴
    public void CardKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            //_currentCard = _cardPooling.GetCardView(_cardData[_playerController.NowSkillIndex]);
        }
    }
   

    
}
