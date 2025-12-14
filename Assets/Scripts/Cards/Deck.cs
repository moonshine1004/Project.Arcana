using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Deck : MonoBehaviour
{
    [SerializeField]private SaveDeck _saveDeck;
    
    //플레이어가 구성한 카드 덱 클래스 입니다
    //카드 덱 리스트
    [SerializeField]private List<CardData> _cardDeck =new List<CardData>(12);
    public List<CardData> cardDeck
    {
        get{return _cardDeck;}
    }


    public async void Start()
    {
        await _saveDeck.initCardData(_cardDeck);
        Debug.Log("카드 불러오기 끝");
        GameManager.LogIn = true;
    }

    public void Update()
    {
        
    }

}
