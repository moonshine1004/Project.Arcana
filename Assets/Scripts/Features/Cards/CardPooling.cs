using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CardPooling : MonoBehaviour
{
    //카드 풀링을 위한 클래스
    [SerializeField] private CardView _cardViewPrefab; //카드 프리팹 위임
    [SerializeField] private Transform _cardParent; //카드가 생성될 부모 오브젝트
    private ObjectPool<CardView> _cardPool; //카드 프리팹 풀
    private void Start()
    {
        //카드 오브젝트 풀 생성
        _cardPool = new ObjectPool<CardView>(
            CreateCard,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyCard,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 10
        );
        //초기 카드 12장 생성
        for (int i = 0; i < 12; i++)
        {
            var card = CreateCard();
            _cardPool.Release(card);
        }
    }
    //카드 생성 메서드
    private CardView CreateCard()
    {
        //카드 프리팹을 인스턴스화 후 반환
        var card = Instantiate(_cardViewPrefab, _cardParent);
        card.gameObject.SetActive(false); //인스턴스화 후 비활성화
        return card; 
    }
    //카드 풀에서 카드를 꺼낼 때(활성화)
    private void OnTakeFromPool(CardView card)
    {
        card.gameObject.SetActive(true);
    }
    //카드를 풀에 반환(비활성화)
    private void OnReturnedToPool(CardView card)
    {
        card.ResetView();
        card.gameObject.SetActive(false);
    }
    //풀의 크기를 초과했을 때 몬스터를 삭제
    private void OnDestroyCard(CardView card)
    {
        Destroy(card.gameObject);
    }

    // 카드 꺼내는 메서드
    public CardView GetCardView(CardData data)
    {
        var card = _cardPool.Get();
        card.Initialize(data);
        return card;
    }

    // 카드 반환 메서드
    public void ReturnCardView(CardView card)
    {
        _cardPool.Release(card);
    }



}
